using System;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Web.WebView2.WinForms;

namespace Client
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
    public partial class Form1 : Form
    {
        private ToolTip toolTip1;
        private WebView2 webViewWeather;

        public Form1()
        {
            InitializeComponent();
            timerDateTime = new System.Windows.Forms.Timer();
            toolTip1 = new ToolTip(); 
            // Khởi tạo WebBrowser
            webViewWeather = new WebView2();
            webViewWeather.Location = new Point(360, 500);
            webViewWeather.Size = new Size(600, 500);
    
            this.Controls.Add(webViewWeather);
            webViewWeather.BringToFront();
            webViewWeather.BackColor = Color.Black;
            webViewWeather.DefaultBackgroundColor = System.Drawing.Color.Black;

            InitializeWebView2Async();
            InitializeDateTimeTimer();
        }
        

       private async void InitializeWebView2Async()
{
    try
    {
        // Khởi tạo môi trường WebView2
        await webViewWeather.EnsureCoreWebView2Async(null);
        
        // Tải file HTML
        string htmlPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\weather.html"));
        
        if (File.Exists(htmlPath))
        {
            webViewWeather.Source = new Uri(htmlPath);
        }
        else
        {
            
            MessageBox.Show("Không tìm thấy file weather.html", "Lỗi", 
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi khi khởi tạo WebView2: {ex.Message}", "Lỗi",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        private System.Windows.Forms.Timer timerDateTime;
        private void InitializeDateTimeTimer()
        {
            //timerDateTime = new System.Windows.Forms.Timer();
            timerDateTime.Interval = 1000;
            timerDateTime.Tick += TimerDateTime_Tick;
            timerDateTime.Start();
            UpdateDateTime();
        }

        private void TimerDateTime_Tick(object? sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            // Sử dụng Invoke nếu cần thiết để tránh cross-thread operation
            if (labDateTime.InvokeRequired || labDateTime2.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    labDateTime.Text = "Giờ: " + DateTime.Now.ToString("HH:mm:ss");
                    labDateTime2.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");
                });
            }
            else
            {
                labDateTime.Text = "Giờ: " + DateTime.Now.ToString("HH:mm:ss");
                labDateTime2.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        // Cập nhật phương thức btnSearch_Click
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string city = TBCity.Text.Trim();
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Vui lòng nhập tên thành phố!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnSearch.Enabled = false;

                // Tạo request object mới bao gồm cả yêu cầu lấy thành phố lân cận
                var request = new
                {
                    city = city,
                    withNearby = true // Yêu cầu lấy thông tin các thành phố lân cận
                };

                string requestJson = JsonConvert.SerializeObject(request);
                string response = await GetWeatherDataFromServerAsync(requestJson);

                // Phân tích response mới
                dynamic responseData = JsonConvert.DeserializeObject(response);

                // Hiển thị thời tiết chính
                DisplayWeatherData(JsonConvert.SerializeObject(responseData.MainWeather));

                // Hiển thị các thành phố lân cận
                DisplayNearbyCities(responseData.NearbyCities.ToObject<List<WeatherResponse>>());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu thời tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSearch.Enabled = true;
            }
        }

        private void DisplayWeatherData(string jsonData)
        {
            try
            {
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonData);

                if (!weatherResponse.Success)
                {
                    MessageBox.Show(weatherResponse.ErrorMessage, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Display current weather
                labDistrict.Text = $"{weatherResponse.City ?? "N/A"}, {weatherResponse.Country ?? "N/A"}"; 
                labTemp_min.Text = $"{weatherResponse.Temp_min}°C";
                labTemp_max.Text = $"{weatherResponse.Temp_max}°C";
                labDetail2.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(weatherResponse.Description.ToLower());
                labTemperature.Text = $"{weatherResponse.Temperature}°C";
                 SetLabelWithTooltip(labHumidity, $"{weatherResponse.Humidity}%", 
                     $"Không khí {(weatherResponse.Humidity > 70 ? "ẩm ướt" : "khô ráo")}");
         
                 SetLabelWithTooltip(labWindSpeed, $"{weatherResponse.WindSpeed} km/h", 
                     $"{(weatherResponse.WindSpeed > 20 ? "Gió mạnh" : "Gió nhẹ")}");
         
                 SetLabelWithTooltip(labPressure, $"{weatherResponse.Pressure} hPa", 
                     $"{(weatherResponse.Pressure < 1000 ? "Có thể có thời tiết xấu" : "Ổn định")}");
         
                 SetLabelWithTooltip(labFeels_like, $"{weatherResponse.Like_feel}°C", 
                     $"{(weatherResponse.Like_feel > weatherResponse.Temperature ? "Nóng hơn thực tế" : "Mát hơn thực tế")}");
 
                 labSunrise.Text = $"{weatherResponse.Sunrise ?? "N/A"}";
                 labSunset.Text = $"{weatherResponse.Sunset ?? "N/A"}";

                if (!string.IsNullOrEmpty(weatherResponse.Icon))
                {
                    picIcon.ImageLocation = $"http://openweathermap.org/img/wn/{weatherResponse.Icon}@2x.png";
                }

                labFeels_like.Text = $"~{weatherResponse.Like_feel}°C";
                labAdvice.Text = GetWeatherAdvice(weatherResponse.Temperature, weatherResponse.Description);

                // Display forecast
                if (weatherResponse.DailyForecast != null && weatherResponse.DailyForecast.Count > 0)
                {
                    DisplayForecastDetails(weatherResponse.DailyForecast);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetLabelWithTooltip(Label label, string mainText, string tooltipText)
         {
             label.Text = mainText;
             toolTip1.SetToolTip(label, tooltipText);
             toolTip1.IsBalloon = true; 
             toolTip1.ShowAlways = true; 
     
         }
        

        private void DisplayForecastDetails(List<DailyForecast> forecasts)
        {
            // Tạo DataTable với các cột cần thiết (đã bỏ cột đầu tiên trống)
            DataTable dt = new DataTable();
            dt.Columns.Add("Ngày");
            dt.Columns.Add("Thứ");
            dt.Columns.Add("Thấp nhất");
            dt.Columns.Add("Cao nhất");
            dt.Columns.Add("Mô tả");
            dt.Columns.Add("Thời tiết", typeof(Image));

            // Thêm dữ liệu vào các hàng (bỏ hàng cuối cùng trống)
            foreach (var forecast in forecasts)
            {
                // Tải hình ảnh từ OpenWeatherMap
                Image weatherIcon = null;
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        byte[] imageData = httpClient.GetByteArrayAsync($"http://openweathermap.org/img/wn/{forecast.Icon}.png").Result;
                        using (var stream = new MemoryStream(imageData))
                        {
                            weatherIcon = Image.FromStream(stream);
                        }
                    }
                }
                catch
                {
                    weatherIcon = null;
                }

                // Viết hoa chữ cái đầu mô tả
                string description = string.IsNullOrEmpty(forecast.Description)
                    ? ""
                    : char.ToUpper(forecast.Description[0]) + forecast.Description.Substring(1);

                dt.Rows.Add(
                    forecast.Date.ToString("dd/MM"),
                    forecast.DayOfWeek,
                    $"{forecast.MinTemperature}°C",
                    $"{forecast.MaxTemperature}°C",
                    description,
                    weatherIcon
                );
            }

            // Gán DataTable vào DataGridView
            dataGridView1.DataSource = dt;

            // Cấu hình hiển thị
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false; // Ẩn cột đầu tiên (cột trống)

            // Cấu hình cột biểu tượng
            // Cấu hình cột biểu tượng
            var imageColumn = dataGridView1.Columns["Thời tiết"] as DataGridViewImageColumn;
            if (imageColumn != null)
            {
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageColumn.DefaultCellStyle.NullValue = null;
                imageColumn.Width = 40;
            }

            // Căn chỉnh nội dung các cột
            dataGridView1.Columns["Ngày"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Thứ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Thấp nhất"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Cao nhất"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Mô tả"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Đặt font cho cột mô tả
            dataGridView1.Columns["Mô tả"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Ẩn dòng trống cuối cùng (nếu có)
            dataGridView1.AllowUserToAddRows = false;
        }


        private string GetWeatherAdvice(double temperature, string description)
        {
            description = description.ToLower();
            string advice = "";

            if (temperature > 35)
                advice = "Nắng cực điểm, tránh ra ngoài giờ cao điểm 11-15h. ";
            else if (temperature > 30)
                advice = "Trời nắng nóng, cần che chắn cẩn thận. ";
            else if (temperature < 10)
                advice = "Trời rét đậm, mặc nhiều lớp áo ấm. ";
            else if (temperature < 20)
                advice = "Tiết trời mát mẻ dễ chịu. ";
            else
                advice = "Thời tiết ôn hòa lý tưởng. ";

            if (description.Contains("mưa lớn"))
                advice += "Mưa to kèm gió mạnh, hạn chế di chuyển. Mang áo mưa loại tốt.";
            else if (description.Contains("mưa") || description.Contains("mây đen"))
                advice += "Trời có mưa, nhớ mang theo ô. Mưa lạnh cần mặc áo khoác.";
            else if (description.Contains("dông") || description.Contains("storm"))
                advice = "Cảnh báo dông bão! Ở trong nhà, tránh cây cối, công trình cao.";
            else if (description.Contains("nắng gắt"))
                advice += "Bôi kem chống nắng, đội mũ rộng vành, uống đủ nước.";
            else if (description.Contains("sương mù"))
                advice += "Sương mù dày đặc, lái xe bật đèn, giảm tốc độ.";
            else if (description.Contains("nhiều mây"))
                advice += "Trời nhiều mây, vẫn cần đề phòng nắng gắt buổi trưa.";

            if (temperature > 30)
                advice += " Uống nhiều nước, ăn đồ mát.";
            else if (temperature < 15)
                advice += " Giữ ấm cổ và tay chân.";

            return advice;
        }

        // Cập nhật phương thức GetWeatherDataFromServerAsync
        private async Task<string> GetWeatherDataFromServerAsync(string requestData)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync("127.0.0.1", 8888);

                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(requestData);
                    await stream.WriteAsync(data, 0, data.Length);

                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(buffer, 0, bytesRead);
                }
            }
        }
        // Thêm phương thức mới để hiển thị các thành phố lân cận
        private void DisplayNearbyCities(List<WeatherResponse> nearbyCities)
        {
            if (nearbyCities == null || nearbyCities.Count == 0)
            {
                dataGridView2.Visible = false;
                return;
            }

            dataGridView2.Visible = true;

            // Tạo DataTable với các cột cần thiết
            DataTable dt = new DataTable();
            dt.Columns.Add("Thành phố");
            dt.Columns.Add("Nhiệt độ");
            dt.Columns.Add("Độ ẩm");
            dt.Columns.Add("Gió");
            dt.Columns.Add("Mô tả");
            dt.Columns.Add("Thời tiết", typeof(Image));

            // Thêm dữ liệu vào các hàng
            foreach (var city in nearbyCities)
            {
                // Tải hình ảnh từ OpenWeatherMap
                Image weatherIcon = null;
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        byte[] imageData = httpClient.GetByteArrayAsync($"http://openweathermap.org/img/wn/{city.Icon}.png").Result;
                        using (var stream = new MemoryStream(imageData))
                        {
                            weatherIcon = Image.FromStream(stream);
                        }
                    }
                }
                catch
                {
                    weatherIcon = null;
                }

                // Viết hoa chữ cái đầu mô tả
                string description = string.IsNullOrEmpty(city.Description)
                    ? ""
                    : char.ToUpper(city.Description[0]) + city.Description.Substring(1);

                dt.Rows.Add(
                    $"{city.City}, {city.Country}",
                    $"{city.Temperature}°C",
                    $"{city.Humidity}%",
                    $"{city.WindSpeed} km/h",
                    description,
                    weatherIcon
                );
            }

            // Gán DataTable vào DataGridView
            dataGridView2.DataSource = dt;

            // Cấu hình hiển thị
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowHeadersVisible = false;

            // Cấu hình cột biểu tượng
            var imageColumn = dataGridView2.Columns["Thời tiết"] as DataGridViewImageColumn;
            if (imageColumn != null)
            {
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageColumn.DefaultCellStyle.NullValue = null;
                imageColumn.Width = 40;
            }

            // Căn chỉnh nội dung các cột
            dataGridView2.Columns["Nhiệt độ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Độ ẩm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Gió"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.AllowUserToAddRows = false;
        }

        private async void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnLocation.Enabled = false;

                // Lấy vị trí hiện tại qua IP
                var location = await GetLocationByIPAsync();

                if (location != null && location.Lat.HasValue && location.Lon.HasValue)
                {
                    TBCity.Text = location.City;

                    // Gửi yêu cầu theo lat/lon thay vì tên thành phố
                    var request = new
                    {
                        lat = location.Lat.Value,
                        lon = location.Lon.Value,
                        withNearby = true
                    };

                    string requestJson = JsonConvert.SerializeObject(request);
                    string response = await GetWeatherDataFromServerAsync(requestJson);

                    // Phân tích response
                    dynamic responseData = JsonConvert.DeserializeObject(response);

                    DisplayWeatherData(JsonConvert.SerializeObject(responseData.MainWeather));
                    DisplayNearbyCities(responseData.NearbyCities.ToObject<List<WeatherResponse>>());
                }
                else
                {
                    MessageBox.Show("Không thể xác định vị trí hiện tại", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy vị trí: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnLocation.Enabled = true;
            }
        }

        private async Task<LocationInfo?> GetLocationByIPAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Sử dụng IP-API.com (miễn phí)
                    var response = await httpClient.GetStringAsync("http://ip-api.com/json/");
                    var locationData = JsonConvert.DeserializeObject<LocationInfo>(response);

                    if (locationData?.Status == "success")
                    {
                        return locationData;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lấy vị trí từ IP: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
    public class LocationInfo
    {
        public string? Status { get; set; }
        public string? City { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
    }


    public class WeatherResponse
    {
        public bool Success { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Pressure { get; set; }
        public double Like_feel { get; set; }

        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Icon { get; set; }
        public string? Sunset { get; set; }
        public string? Sunrise { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Temp_min { get; set; }
        public string? Temp_max { get; set; }

        public List<DailyForecast> DailyForecast { get; set; } = new List<DailyForecast>();
        public Coordinates? Coordinates { get; set; }

    }
    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public string? DayOfWeek { get; set; }
        public double AvgTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }
}