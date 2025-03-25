using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherClient
{
    public partial class Form1 : Form
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 8888;
        private readonly HttpClient _httpClient = new HttpClient();

        // Các controls
        private TextBox txtCity = new TextBox();
        private Button btnGetWeather = new Button();
        private PictureBox pictureBox1 = new PictureBox();
        private Label lblCity = new Label();
        private Label lblTemp = new Label();
        private Label lblHumidity = new Label();
        private Label lblDescription = new Label();
        private Label lblDate = new Label();
        private Panel panel1 = new Panel();

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("vi-VN"));
        }

        private void InitializeCustomComponents()
        {
            // Cấu hình controls
            txtCity.Location = new Point(20, 20);
            txtCity.Width = 200;
            
            btnGetWeather.Text = "Xem thời tiết";
            btnGetWeather.Location = new Point(230, 20);
            btnGetWeather.Click += BtnGetWeather_Click;
            
            pictureBox1.Location = new Point(20, 60);
            pictureBox1.Size = new Size(100, 100);
            
            // Cấu hình các label khác...
            
            // Thêm controls vào form
            Controls.Add(txtCity);
            Controls.Add(btnGetWeather);
            Controls.Add(pictureBox1);
            Controls.Add(lblCity);
            Controls.Add(lblTemp);
            Controls.Add(lblHumidity);
            Controls.Add(lblDescription);
            Controls.Add(lblDate);
            Controls.Add(panel1);
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
                        // Gửi yêu cầu
                        byte[] request = Encoding.UTF8.GetBytes(txtCity.Text.Trim());
                        await stream.WriteAsync(request, 0, request.Length);

                        // Nhận phản hồi
                        byte[] buffer = new byte[4096];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // Sử dụng Newtonsoft.Json thay vì System.Text.Json
                        var settings = new JsonSerializerSettings 
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        
                        var weather = JsonConvert.DeserializeObject<WeatherResponse>(jsonResponse, settings);

                        if (weather?.Success == true)
                        {
                            await DisplayWeather(weather);
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

        private async Task DisplayWeather(WeatherResponse weather)
{
    await Task.Run(() => 
    {
        Invoke((MethodInvoker)(() =>
        {
            lblCity.Text = weather.City;
            lblTemp.Text = $"{weather.Temperature:0.#}°C";
            lblHumidity.Text = $"{weather.Humidity}%";
            lblDescription.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(weather.Description);
            panel1.BackColor = weather.Temperature > 30 ? Color.OrangeRed :
                            weather.Temperature < 15 ? Color.LightSkyBlue :
                            Color.LightGreen;
        }));

        if (!string.IsNullOrEmpty(weather.Icon))
        {
            try
            {
                var iconUrl = $"https://openweathermap.org/img/wn/{weather.Icon}@2x.png";
                var imageBytes = _httpClient.GetByteArrayAsync(iconUrl).Result;
                using (var ms = new MemoryStream(imageBytes))
                {
                    var image = Image.FromStream(ms);
                    Invoke((MethodInvoker)(() => pictureBox1.Image = image));
                }
            }
            catch
            {
                Invoke((MethodInvoker)(() => pictureBox1.Image = null));
            }
        }
    });
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