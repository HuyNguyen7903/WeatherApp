using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherClient
{
    public partial class Form1 : Form
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 8888;
        private readonly HttpClient _httpClient = new HttpClient();

        // Các controls với khởi tạo ngay khi khai báo
        private TextBox txtCity = new TextBox();
        private Button btnGetWeather = new Button();
        private PictureBox pictureBox1 = new PictureBox();
        private Label lblCity = new Label();
        private Label lblTemp = new Label();
        private Label lblHumidity = new Label();
        private Label lblDescription = new Label();
        private Label lblDate = new Label();
        private Panel panelWeather = new Panel();

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            UpdateDate();
        }

        private void InitializeCustomComponents()
        {
            // Thiết lập form
            this.ClientSize = new Size(350, 250);
            this.Text = "Weather App";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // TextBox nhập thành phố
            txtCity.Location = new Point(20, 20);
            txtCity.Size = new Size(200, 25);
            txtCity.Font = new Font("Segoe UI", 10);
            txtCity.PlaceholderText = "Nhập tên thành phố";

            // Nút Xem thời tiết
            btnGetWeather.Text = "Xem thời tiết";
            btnGetWeather.Location = new Point(230, 20);
            btnGetWeather.Size = new Size(100, 25);
            btnGetWeather.BackColor = Color.SteelBlue;
            btnGetWeather.ForeColor = Color.White;
            btnGetWeather.FlatStyle = FlatStyle.Flat;
            btnGetWeather.Font = new Font("Segoe UI", 9);
            btnGetWeather.FlatAppearance.BorderSize = 0;
            btnGetWeather.Click += BtnGetWeather_Click;

            // Panel hiển thị thông tin
            panelWeather.Location = new Point(20, 60);
            panelWeather.Size = new Size(310, 170);
            panelWeather.BorderStyle = BorderStyle.FixedSingle;
            panelWeather.BackColor = Color.AliceBlue;

            // Label thành phố
            lblCity.Location = new Point(20, 15);
            lblCity.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblCity.AutoSize = true;

            // Label nhiệt độ
            lblTemp.Location = new Point(20, 45);
            lblTemp.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblTemp.AutoSize = true;

            // Label độ ẩm
            lblHumidity.Location = new Point(20, 85);
            lblHumidity.Font = new Font("Segoe UI", 11);
            lblHumidity.AutoSize = true;

            // Label mô tả
            lblDescription.Location = new Point(20, 110);
            lblDescription.Font = new Font("Segoe UI", 11);
            lblDescription.AutoSize = true;

            // Label ngày tháng
            lblDate.Location = new Point(20, 135);
            lblDate.Font = new Font("Segoe UI", 9);
            lblDate.AutoSize = true;

            // PictureBox biểu tượng thời tiết
            pictureBox1.Location = new Point(200, 20);
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // Thêm controls vào panel
            panelWeather.Controls.Add(lblCity);
            panelWeather.Controls.Add(lblTemp);
            panelWeather.Controls.Add(lblHumidity);
            panelWeather.Controls.Add(lblDescription);
            panelWeather.Controls.Add(lblDate);
            panelWeather.Controls.Add(pictureBox1);

            // Thêm controls vào form
            this.Controls.Add(txtCity);
            this.Controls.Add(btnGetWeather);
            this.Controls.Add(panelWeather);
        }

        private void UpdateDate()
        {
            lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("vi-VN"));
        }

        private async void BtnGetWeather_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thành phố", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnGetWeather.Enabled = false;
                btnGetWeather.Text = "Đang tải...";
                Cursor = Cursors.WaitCursor;

                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(ServerIp, Port);
                    using (var stream = client.GetStream())
                    {
                        string city = txtCity.Text.Trim();
                        byte[] requestData = Encoding.UTF8.GetBytes(city);
                        
                        // Gửi độ dài trước
                        byte[] lengthBytes = BitConverter.GetBytes(requestData.Length);
                        await stream.WriteAsync(lengthBytes.AsMemory(0, 4));
                        await stream.WriteAsync(requestData.AsMemory(0, requestData.Length));

                        // Nhận phản hồi
                        byte[] lengthBuffer = new byte[4];
                        int bytesRead = await ReadExactAsync(stream, lengthBuffer, 0, 4);
                        if (bytesRead != 4) throw new Exception("Invalid response length");
                        
                        int responseLength = BitConverter.ToInt32(lengthBuffer, 0);
                        byte[] responseBuffer = new byte[responseLength];
                        bytesRead = await ReadExactAsync(stream, responseBuffer, 0, responseLength);
                        if (bytesRead != responseLength) throw new Exception("Invalid response data");

                        string jsonResponse = Encoding.UTF8.GetString(responseBuffer);
                        var weather = JsonConvert.DeserializeObject<WeatherResponse>(jsonResponse);
                        
                        if (weather?.Success == true)
                        {
                            DisplayWeather(weather);
                        }
                        else
                        {
                            MessageBox.Show(weather?.Error ?? "Lỗi không xác định từ server", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Không thể kết nối đến server", 
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGetWeather.Enabled = true;
                btnGetWeather.Text = "Xem thời tiết";
                Cursor = Cursors.Default;
            }
        }

        private async Task<int> ReadExactAsync(NetworkStream stream, byte[] buffer, int offset, int count)
        {
            int totalRead = 0;
            while (totalRead < count)
            {
                int read = await stream.ReadAsync(buffer.AsMemory(offset + totalRead, count - totalRead));
                if (read == 0) break;
                totalRead += read;
            }
            return totalRead;
        }

        private void DisplayWeather(WeatherResponse weather)
        {
            lblCity.Text = weather.City;
            lblTemp.Text = $"{weather.Temperature:0}°C";
            lblHumidity.Text = $"Độ ẩm: {weather.Humidity}%";
            lblDescription.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(weather.Description);
            UpdateDate();

            // Đổi màu nền theo nhiệt độ
            panelWeather.BackColor = weather.Temperature > 30 ? Color.LightCoral :
                                  weather.Temperature < 15 ? Color.LightCyan :
                                  Color.Honeydew;

            // Tải biểu tượng thời tiết
            if (!string.IsNullOrEmpty(weather.Icon))
            {
                try
                {
                    var iconUrl = $"https://openweathermap.org/img/wn/{weather.Icon}@2x.png";
                    var imageBytes = _httpClient.GetByteArrayAsync(iconUrl).Result;
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    pictureBox1.Image = null;
                }
            }
        }
    }

    public class WeatherResponse
    {
        public bool Success { get; set; }
        public string City { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}