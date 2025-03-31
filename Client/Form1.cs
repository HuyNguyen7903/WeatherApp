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
namespace Client
{
    public partial class Form1 : Form
    {
        private ToolTip toolTip1;
        public Form1()
        {
            InitializeComponent();
            toolTip1 = new ToolTip(); 
            InitializeChart();
            UpdateDateTime();
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

        private void UpdateDateTime()
        {
            labDateTime.Text = "Giờ: " + DateTime.Now.ToString("HH:mm:ss");
            labDateTime2.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");
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

                if (weatherResponse == null || !weatherResponse.Success)
                {
                    MessageBox.Show(weatherResponse?.ErrorMessage ?? "Không thể đọc dữ liệu thời tiết", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị thông tin cơ bản
                labTemperature.Text = $"{weatherResponse.Temperature}°C";
                labDistrict.Text = $"{weatherResponse.City ?? "N/A"}, {weatherResponse.Country ?? "N/A"}";
                labDetail2.Text = weatherResponse.Description ?? "N/A";

                SetLabelWithTooltip(labHumidity, $"Độ ẩm: {weatherResponse.Humidity}%", 
                    $"Không khí {(weatherResponse.Humidity > 70 ? "ẩm ướt" : "khô ráo")}");
        
                SetLabelWithTooltip(labWindSpeed, $"Gió: {weatherResponse.WindSpeed} km/h", 
                    $"{(weatherResponse.WindSpeed > 20 ? "Gió mạnh" : "Gió nhẹ")}");
        
                SetLabelWithTooltip(labPressure, $"Áp suất: {weatherResponse.Pressure} hPa", 
                    $"{(weatherResponse.Pressure < 1000 ? "Có thể có thời tiết xấu" : "Ổn định")}");
        
                SetLabelWithTooltip(labFeels_like, $"{weatherResponse.Like_feel}°C", 
                    $"{(weatherResponse.Like_feel > weatherResponse.Temperature ? "Nóng hơn thực tế" : "Mát hơn thực tế")}");

                labSunrise.Text = $"Mọc: {weatherResponse.Sunrise ?? "N/A"}";
                labSunset.Text = $"Lặn: {weatherResponse.Sunset ?? "N/A"}";

                if (!string.IsNullOrEmpty(weatherResponse.Icon))
                {
                    picIcon.ImageLocation = $"http://openweathermap.org/img/wn/{weatherResponse.Icon}@2x.png";
                }

                labAdvice.Text = GetWeatherAdvice(weatherResponse.Temperature, weatherResponse.Description ?? string.Empty);

                // Hiển thị dự báo
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
                ChartType = SeriesChartType.Line,
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
            DataTable dt = new DataTable();
            dt.Columns.Add("Ngày");
            dt.Columns.Add("Thứ");
            dt.Columns.Add("Nhiệt độ TB");
            dt.Columns.Add("Thấp nhất");
            dt.Columns.Add("Cao nhất");
            dt.Columns.Add("Thời tiết");

            foreach (var forecast in forecasts)
            {
                dt.Rows.Add(
                    forecast.Date.ToString("dd/MM"),
                    forecast.DayOfWeek,
                    $"{forecast.AvgTemperature}°C",
                    $"{forecast.MinTemperature}°C",
                    $"{forecast.MaxTemperature}°C",
                    forecast.Description
                );
            }

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private string GetWeatherAdvice(double temperature, string description)
        {
            if (temperature > 35)
                return "Trời rất nóng, có thể oi bức và khó chịu. Nhiệt độ cao dễ gây mất nước và mệt mỏi.";
            else if (temperature > 30)
                return "Thời tiết nóng với nhiệt độ cao, có thể có nắng gắt vào ban ngày.";
            else if (temperature >= 20 && temperature <= 30)
                return "Thời tiết ấm áp, không quá nóng cũng không quá lạnh, thích hợp cho các hoạt động ngoài trời.";
            else if (temperature >= 10 && temperature < 20)
                return "Trời mát, có thể se lạnh vào sáng sớm hoặc ban đêm.";
            else
                return "Trời lạnh, nhiệt độ thấp có thể gây rét buốt, nhất là vào ban đêm.";
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng lấy vị trí hiện tại chưa được triển khai!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class WeatherResponse
    {
        public bool Success { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Pressure { get; set; }
        public double Like_feel { get; set; }

        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Sunset { get; set; } = string.Empty;
        public string Sunrise { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public List<DailyForecast> DailyForecast { get; set; } = new List<DailyForecast>();
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public double AvgTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}

