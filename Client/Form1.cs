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

namespace Client
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
    public partial class Form1 : Form
    {
         private ToolTip toolTip1;
        public Form1()
        {
            InitializeComponent();
            timerDateTime = new System.Windows.Forms.Timer();
            toolTip1 = new ToolTip(); 
            InitializeChart();
            InitializeDateTimeTimer();
        }

        private void InitializeChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // Create chart area
            ChartArea chartArea = new ChartArea("ForecastArea");
            chartArea.AxisX.Title = "Ngày";
            chartArea.AxisY.Title = "Nhiệt độ (°C)";
            chartArea.AxisX.Interval = 1;
            chart1.ChartAreas.Add(chartArea);

            // Add legend
            Legend legend = new Legend();
            chart1.Legends.Add(legend);
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

                string response = await GetWeatherDataFromServerAsync(city);
                DisplayWeatherData(response);
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
                labDistrict.Text = $"{weatherResponse.City ?? "N/A"}, {weatherResponse.Country ?? "N/A"}"; labTemp_min.Text = $"{weatherResponse.Temp_min}°C";
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
                    DisplayForecastChart(weatherResponse.DailyForecast);
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
        private void DisplayForecastChart(List<DailyForecast> forecasts)
        {
            chart1.Series.Clear();
            // Add average temperature series
            Series avgSeries = new Series("Nhiệt độ TB")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.DeepSkyBlue,
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };

            // Add min/max series
            Series minSeries = new Series("Thấp nhất")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };

            Series maxSeries = new Series("Cao nhất")
            {
                ChartType = SeriesChartType.Point,
                Color = Color.Red,
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };

            foreach (var forecast in forecasts)
            {
                string dayLabel = forecast.Date.ToString("dd/MM");
                avgSeries.Points.AddXY(dayLabel, forecast.AvgTemperature);
                minSeries.Points.AddXY(dayLabel, forecast.MinTemperature);
                maxSeries.Points.AddXY(dayLabel, forecast.MaxTemperature);
            }

            chart1.Series.Add(avgSeries);
            chart1.Series.Add(minSeries);
            chart1.Series.Add(maxSeries);

            chart1.Titles.Clear();
            chart1.Titles.Add("DỰ BÁO THỜI TIẾT 7 NGÀY");
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

        private async Task<string> GetWeatherDataFromServerAsync(string city)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync("127.0.0.1", 8888);

                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(city);
                    await stream.WriteAsync(data, 0, data.Length);

                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(buffer, 0, bytesRead);
                }
            }
        }


        private async void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnLocation.Enabled = false;

                // Lấy vị trí hiện tại qua IP
                var location = await GetLocationByIPAsync();

                if (location != null)
                {
                    TBCity.Text = location.City;

                    // Tự động gọi hàm lấy thời tiết
                    string response = await GetWeatherDataFromServerAsync(location.City);
                    DisplayWeatherData(response);
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