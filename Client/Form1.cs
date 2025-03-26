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

        public Form1()
        {
            InitializeComponent();
            UpdateDate();
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
