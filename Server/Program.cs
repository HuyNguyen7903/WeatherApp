using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace WeatherServer
{
    class Program
    {
        private const string OpenWeatherMapApiKey = "26653ec7090961b6a70cc1709679d31d";
        private const string OpenWeatherMapBaseUrl = "http://api.openweathermap.org/data/2.5/weather";
        private const string OpenWeatherForecastUrl = "http://api.openweathermap.org/data/2.5/forecast";

        static async Task Main(string[] args)
        {   // Khởi tạo TCP Server
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            server.Start();
            Console.WriteLine("Server started on port 8888...");
            // ✅ Kiểm tra kết nối chatbot server tại localhost:1234
            bool chatbotAvailable = await CheckChatbotConnectionAsync();
            Console.WriteLine(chatbotAvailable
                ? "✅ Chatbot server is available at port 1234."
                : "❌ Không thể kết nối chatbot tại port 1234.");
            // Nếu không kết nối được đến chatbot server, dừng server
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }       
        }
        // Kiểm tra kết nối đến một chatbot local
        private static async Task<bool> CheckChatbotConnectionAsync()
        {
            try
            {   // Gửi một yêu cầu thử nghiệm đến chatbot server
                using (HttpClient client = new HttpClient())
                {   // Tạo một yêu cầu thử nghiệm
                    var testMessage = new
                    {
                        model = "vistral-7b-chat",
                        messages = new[] { new { role = "user", content = "ping" } }
                    };
                    // Chuyển đổi yêu cầu thành JSON
                    var content = new StringContent(JsonConvert.SerializeObject(testMessage), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:1234/v1/chat/completions", content);
                    // Trả về true nếu request thành công
                    return response.IsSuccessStatusCode;
                }
            }
            // Xử lý ngoại lệ
            catch
            {
                return false;
            }
        }

        public class ChatbotApiClient
        {
            private readonly HttpClient _httpClient;
            private const string ApiBaseUrl = "http://localhost:1234/v1/chat/completions";        
            public ChatbotApiClient()
            {
                _httpClient = new HttpClient();
            }
            public async Task<string> GetChatbotResponse(string message)
            {
                try
                {   // Tạo một yêu cầu đến chatbot server
                    var request = new
                    {
                        model = "vistral-7b-chat",
                        messages = new[] { new { role = "user", content = message } }
                    };
                    // Chuyển đổi yêu cầu thành JSON
                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(ApiBaseUrl, content);
                    response.EnsureSuccessStatusCode();
                    // Đọc phản hồi từ chatbot
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(result);
                    return data?.choices?[0]?.message?.content ?? "Chatbot không phản hồi.";
                }
                // Xử lý ngoại lệ
                catch (Exception ex)
                {
                    Console.WriteLine($"Chatbot error: {ex.Message}");
                    return $"Lỗi khi kết nối với chatbot: {ex.Message}";
                }
            }
        }
        // xử lý các kết nối từ client trong một TCP server
        static async Task HandleClientAsync(TcpClient client)
        {
            try
            {   // Khởi tạo và thiết lập kết nối
                using (client)
                using (NetworkStream stream = client.GetStream())
                {   // Đọc dữ liệu từ client
                    byte[] buffer = new byte[2048];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string requestData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    try
                    {   // Xử lý yêu cầu
                        dynamic request = JsonConvert.DeserializeObject(requestData);
                        // Xử lý yêu cầu chatbot
                        if (request?.chat != null)
                        {
                            var chatbot = new ChatbotApiClient();
                            string botReply = await chatbot.GetChatbotResponse((string)request.chat);
                            var responseObj = new { response = botReply };
                            await SendJsonResponse(stream, responseObj);
                            return;
                        }
                        // Xử lý yêu cầu thời tiết
                        WeatherResponse weatherResponse;
                        List<WeatherResponse> nearbyCities = new List<WeatherResponse>();
                        // Xử lý yêu cầu theo tên thành phố 
                        if (request?.city != null)
                        {
                        weatherResponse = await GetRealWeatherData((string)request.city);
                            if (weatherResponse.Success && request?.withNearby == true && weatherResponse.Coordinates != null)
                            {
                                nearbyCities = await GetNearbyCitiesWeatherData(
                                    weatherResponse.Coordinates.Latitude,
                                    weatherResponse.Coordinates.Longitude);
                            }
                        }
                        // Xử lý yêu cầu theo tọa độ
                        else if (request?.lat != null && request?.lon != null)
                        {
                            weatherResponse = await GetWeatherByCoordinates((double)request.lat, (double)request.lon);
                            if (weatherResponse.Success && request?.withNearby == true)
                            {
                                nearbyCities = await GetNearbyCitiesWeatherData((double)request.lat, (double)request.lon);
                            }
                        }
                        // Nếu không có yêu cầu hợp lệ
                        else
                        {   //
                            weatherResponse = new WeatherResponse 
                            { 
                                Success = false, 
                                ErrorMessage = "Yêu cầu không hợp lệ. Vui lòng cung cấp thành phố hoặc tọa độ." 
                            };
                        }

                        var fullResponse = new
                        {
                        MainWeather = weatherResponse,
                            NearbyCities = nearbyCities
                        };
                        // Gửi phản hồi về client
                        await SendJsonResponse(stream, fullResponse);
                    }
                    // Xử lý ngoại lệ
                    catch (JsonException)
                    {
                        var errorResponse = new { error = "Định dạng JSON không hợp lệ" };
                        await SendJsonResponse(stream, errorResponse);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing request: {ex.Message}");
                        var errorResponse = new { error = $"Lỗi xử lý yêu cầu: {ex.Message}" };
                        await SendJsonResponse(stream, errorResponse);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
        // Chuyển đổi timestamp(dạng số) sang giá trị ngày giờ(DateTime)
        private static DateTime ConvertDateTime(long timestamp)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return dateTimeOffset.LocalDateTime;
        }
        // Gửi phản hồi dạng JSON từ server tới client
        private static async Task SendJsonResponse(NetworkStream stream, object responseObj)
        {
            string jsonResponse = JsonConvert.SerializeObject(responseObj);
            byte[] responseData = Encoding.UTF8.GetBytes(jsonResponse);
            await stream.WriteAsync(responseData, 0, responseData.Length);
        }

        static async Task<WeatherResponse> GetRealWeatherData(string city)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Thời tiết hiện tại
                    string currentUrl = $"{OpenWeatherMapBaseUrl}?q={Uri.EscapeDataString(city)}&appid={OpenWeatherMapApiKey}&units=metric&lang=vi";
                    HttpResponseMessage currentResponse = await httpClient.GetAsync(currentUrl);
                    // kiểm tra lỗi và đọc dữ liệu JSON
                    if (!currentResponse.IsSuccessStatusCode)
                    {
                        string errorContent = await currentResponse.Content.ReadAsStringAsync();
                        throw new Exception($"API request failed: {currentResponse.StatusCode}, {errorContent}");
                    }
                    // Xử lý phản hồi thành công
                    string currentJson = await currentResponse.Content.ReadAsStringAsync();
                    dynamic currentData = JsonConvert.DeserializeObject(currentJson);
                    // Lấy thông tin thời gian mặt trời mọc/lặn
                    DateTime sunriseTime = ConvertDateTime((long)currentData.sys.sunrise);
                    DateTime sunsetTime = ConvertDateTime((long)currentData.sys.sunset);
                    // Forecast
                    string forecastUrl = $"{OpenWeatherForecastUrl}?q={Uri.EscapeDataString(city)}&appid={OpenWeatherMapApiKey}&units=metric&lang=vi&cnt=40";
                    HttpResponseMessage forecastResponse = await httpClient.GetAsync(forecastUrl);
                    // Kiểm tra và xử lý phản hồi từ API dự báo thời tiết
                    if (!forecastResponse.IsSuccessStatusCode)
                    {
                        string errorContent = await forecastResponse.Content.ReadAsStringAsync();
                        throw new Exception($"Forecast API failed: {forecastResponse.StatusCode}, {errorContent}");
                    }
                    // Đọc dữ liệu JSON từ phản hồi
                    string forecastJson = await forecastResponse.Content.ReadAsStringAsync();
                    dynamic forecastData = JsonConvert.DeserializeObject(forecastJson);
                    // Xử lý dữ liệu dự báo
                    var dailyForecast = ProcessDailyForecast(forecastData.list);
                    // Lấy nhiệt độ min/max từ dự báo ngày hôm nay
                    DailyForecast todayForecast = null;
                    foreach (var forecast in dailyForecast)
                    {
                        if (forecast.Date.Date == DateTime.Today)
                        {
                            todayForecast = forecast;
                            break;
                        }
                    }
                    // Nếu không tìm thấy dự báo cho hôm nay, lấy dự báo đầu tiên
                    if (todayForecast == null && dailyForecast.Count > 0)
                    {
                        todayForecast = dailyForecast[0];
                    }
                    // Sử dụng giá trị từ dự báo nếu có, nếu không thì dùng giá trị từ current data
                    string todayMinTemp = todayForecast != null ?
                        Math.Round(todayForecast.MinTemperature, 0).ToString() :
                        Math.Round((double)currentData.main.temp_min, 0).ToString();
                    string todayMaxTemp = todayForecast != null ?
                        Math.Round(todayForecast.MaxTemperature, 0).ToString() :
                        Math.Round((double)currentData.main.temp_max, 0).ToString();
                    // Tạo đối tượng trả về
                    return new WeatherResponse
                    {
                        Success = true,
                        Temperature = Math.Round((double)currentData.main.temp, 0),
                        Humidity = (int)currentData.main.humidity,
                        WindSpeed = Math.Round((double)currentData.wind.speed * 3.6, 1),
                        Pressure = (double)currentData.main.pressure,
                        Description = (string)currentData.weather[0].description,
                        City = (string)currentData.name,
                        Country = (string)currentData.sys.country,
                        Icon = (string)currentData.weather[0].icon,
                        Sunrise = sunriseTime.ToString("HH:mm"),
                        Sunset = sunsetTime.ToString("HH:mm"),
                        Like_feel = Math.Round((double)currentData.main.feels_like, 0),
                        Temp_min = todayMinTemp,
                        Temp_max = todayMaxTemp,
                        DailyForecast = dailyForecast,
                        Coordinates = new Coordinates
                        {
                            Latitude = (double)currentData.coord.lat,
                            Longitude = (double)currentData.coord.lon
                        }
                    };
                }
                // Xử lý ngoại lệ
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting weather data: {ex}");
                    return new WeatherResponse
                    {
                        Success = false,
                        ErrorMessage = "Không thể lấy dữ liệu thời tiết. Vui lòng thử lại sau."
                    };
                }
            }
        }
        // Lấy thông tin thời tiết hiện tại dựa trên tọa độ địa lý (vĩ độ/kinh độ) từ OpenWeatherMap API
        static async Task<WeatherResponse> GetWeatherByCoordinates(double lat, double lon)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Gọi API để lấy thông tin thời tiết hiện tại
                    string currentUrl = $"{OpenWeatherMapBaseUrl}?lat={lat}&lon={lon}&appid={OpenWeatherMapApiKey}&units=metric&lang=vi";
                    HttpResponseMessage currentResponse = await httpClient.GetAsync(currentUrl);
                    // Xử lý phản hồi nếu bị lỗi
                    if (!currentResponse.IsSuccessStatusCode)
                    {
                        string errorContent = await currentResponse.Content.ReadAsStringAsync();
                        throw new Exception($"API request failed: {currentResponse.StatusCode}, {errorContent}");
                    }
                    // Đọc dữ liệu JSON từ phản hồi thành công
                    string currentJson = await currentResponse.Content.ReadAsStringAsync();
                    dynamic currentData = JsonConvert.DeserializeObject(currentJson);
                    // Lấy thông tin thời gian mặt trời mọc/lặn
                    DateTime sunriseTime = ConvertDateTime((long)currentData.sys.sunrise);
                    DateTime sunsetTime = ConvertDateTime((long)currentData.sys.sunset);
                    // Tạo đối tượng kết quả
                    return new WeatherResponse
                    {
                        Success = true,
                        Temperature = Math.Round((double)currentData.main.temp, 0),
                        Humidity = (int)currentData.main.humidity,
                        WindSpeed = Math.Round((double)currentData.wind.speed * 3.6, 1),
                        Pressure = (double)currentData.main.pressure,
                        Description = (string)currentData.weather[0].description,
                        City = (string)currentData.name,
                        Country = (string)currentData.sys.country,
                        Icon = (string)currentData.weather[0].icon,
                        Sunrise = sunriseTime.ToString("HH:mm"),
                        Sunset = sunsetTime.ToString("HH:mm"),
                        Like_feel = Math.Round((double)currentData.main.feels_like, 0),
                        Temp_min = Math.Round((double)currentData.main.temp_min, 0).ToString(),
                        Temp_max = Math.Round((double)currentData.main.temp_max, 0).ToString(),
                        Coordinates = new Coordinates
                        {
                            Latitude = (double)currentData.coord.lat,
                            Longitude = (double)currentData.coord.lon
                        }
                    };
                }
                // Xử lý ngoại lệ
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting weather by coordinates: {ex}");
                    return new WeatherResponse
                    {
                        Success = false,
                        ErrorMessage = "Không thể lấy dữ liệu thời tiết. Vui lòng thử lại sau."
                    };
                }
            }
        }
        // Lấy thông tin thời tiết của các thành phố lân cận dựa trên tọa độ địa lý (vĩ độ/kinh độ) từ OpenWeatherMap API
        private static async Task<List<WeatherResponse>> GetNearbyCitiesWeatherData(double lat, double lon)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Lấy danh sách các thành phố trong bán kính 50km
                    string nearbyUrl = $"http://api.openweathermap.org/data/2.5/find?lat={lat}&lon={lon}&cnt=5&appid={OpenWeatherMapApiKey}&units=metric&lang=vi";
                    HttpResponseMessage nearbyResponse = await httpClient.GetAsync(nearbyUrl);
                    // Xử lý phản hồi nếu bị lỗi
                    if (!nearbyResponse.IsSuccessStatusCode)
                    {
                        string errorContent = await nearbyResponse.Content.ReadAsStringAsync();
                        throw new Exception($"Nearby cities API failed: {nearbyResponse.StatusCode}, {errorContent}");
                    }
                    // Đọc dữ liệu JSON từ phản hồi thành công
                    string nearbyJson = await nearbyResponse.Content.ReadAsStringAsync();
                    dynamic nearbyData = JsonConvert.DeserializeObject(nearbyJson);

                    var nearbyCities = new List<WeatherResponse>();
                    // Xây dựng danh sách cac thành phố lân cận
                    foreach (var city in nearbyData.list)
                    {
                        var weatherResponse = new WeatherResponse
                        {
                            Success = true,
                            Temperature = Math.Round((double)city.main.temp, 0),
                            Humidity = (int)city.main.humidity,
                            WindSpeed = Math.Round((double)city.wind.speed * 3.6, 1),
                            Pressure = (double)city.main.pressure,
                            Description = (string)city.weather[0].description,
                            City = (string)city.name,
                            Country = (string)city.sys?.country ?? "N/A",
                            Icon = (string)city.weather[0].icon,
                            Like_feel = Math.Round((double)city.main.feels_like, 0),
                            Temp_min = Math.Round((double)city.main.temp_min, 0).ToString(),
                            Temp_max = Math.Round((double)city.main.temp_max, 0).ToString()
                        };
                        nearbyCities.Add(weatherResponse);
                    }
                    return nearbyCities;
                }
                // Xử lý ngoại lệ
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting nearby cities data: {ex}");
                    return new List<WeatherResponse>();
                }
            }
        }
        // Xử lý dữ liệu dự báo thời tiết theo ngày từ API OpenWeatherMap
        private static List<DailyForecast> ProcessDailyForecast(dynamic forecastList)
        {
            var dailyForecasts = new List<DailyForecast>();
            // Nhóm dữ liệu theo ngày
            var groupedByDay = new Dictionary<string, List<dynamic>>();
            foreach (var item in forecastList)
            {
                DateTime dt = DateTime.Parse(item.dt_txt.ToString());
                string dateKey = dt.ToString("yyyy-MM-dd");

                if (!groupedByDay.ContainsKey(dateKey))
                {
                    groupedByDay[dateKey] = new List<dynamic>();
                }
                groupedByDay[dateKey].Add(item);
            }
            // Tính toán thông số từng ngày
            foreach (var day in groupedByDay)
            {
                double avgTemp = 0;
                double minTemp = double.MaxValue;
                double maxTemp = double.MinValue;
                string description = "";
                string icon = "";
                foreach (var item in day.Value)
                {
                    double temp = (double)item.main.temp;
                    avgTemp += temp;
                    minTemp = Math.Min(minTemp, (double)item.main.temp_min);
                    maxTemp = Math.Max(maxTemp, (double)item.main.temp_max);

                    if (string.IsNullOrEmpty(description))
                    {
                        description = item.weather[0].description;
                        icon = item.weather[0].icon;
                    }
                }
                avgTemp /= day.Value.Count;
                // Tạo đối tượng DailyForecast
                dailyForecasts.Add(new DailyForecast
                {
                    Date = DateTime.Parse(day.Key),
                    DayOfWeek = DateTime.Parse(day.Key).ToString("dddd"),
                    AvgTemperature = Math.Round(avgTemp, 1),
                    MinTemperature = Math.Round(minTemp, 1),
                    MaxTemperature = Math.Round(maxTemp, 1),
                    Description = description,
                    Icon = icon
                });
            }
            // Trả về danh sách dự báo thời tiết hàng ngày
            return dailyForecasts.OrderBy(d => d.Date).Take(7).ToList();
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