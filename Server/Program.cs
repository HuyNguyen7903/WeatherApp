using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherServer
{
    public class WeatherServer
    {
        private const int Port = 8888;
        private const string ApiKey = "26653ec7090961b6a70cc1709679d31d";
        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

        public async Task StartAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    // Đọc độ dài dữ liệu
                    byte[] lengthBytes = new byte[4];
                    await stream.ReadAsync(lengthBytes, 0, 4);
                    int length = BitConverter.ToInt32(lengthBytes, 0);
                    
                    // Đọc dữ liệu thành phố
                    byte[] buffer = new byte[length];
                    await stream.ReadAsync(buffer, 0, length);
                    string city = Encoding.UTF8.GetString(buffer);

                    Console.WriteLine($"Received request for city: {city}");

                    string weatherData = await GetWeatherDataAsync(city);
                    
                    // Gửi độ dài trước
                    byte[] responseData = Encoding.UTF8.GetBytes(weatherData);
                    byte[] responseLength = BitConverter.GetBytes(responseData.Length);
                    await stream.WriteAsync(responseLength, 0, 4);
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task<string> GetWeatherDataAsync(string city)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string encodedCity = Uri.EscapeDataString(city);
                    string url = string.Format(ApiUrl, encodedCity, ApiKey);
                    
                    Console.WriteLine($"Calling API: {url}");
                    
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    dynamic data = JsonConvert.DeserializeObject(json);
                    if (data?.cod == 200)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            Success = true,
                            City = city,
                            Temperature = data.main.temp,
                            Humidity = data.main.humidity,
                            Description = data.weather[0].description,
                            Icon = data.weather[0].icon
                        });
                    }
                    return JsonConvert.SerializeObject(new
                    {
                        Success = false,
                        Error = $"Không tìm thấy thành phố {city}!"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return JsonConvert.SerializeObject(new
                {
                    Success = false,
                    Error = "Lỗi khi lấy dữ liệu thời tiết"
                });
            }
        }
    }

    class Program
    {
        static async Task Main()
        {
            WeatherServer server = new WeatherServer();
            await server.StartAsync();
        }
    }
}