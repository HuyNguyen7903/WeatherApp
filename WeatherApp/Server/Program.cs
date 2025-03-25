// Server/Program.cs
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
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string city = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    string weatherData = await GetWeatherDataAsync(city);
                    byte[] response = Encoding.UTF8.GetBytes(weatherData);
                    await stream.WriteAsync(response, 0, response.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task<string> GetWeatherDataAsync(string city)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = string.Format(ApiUrl, city, ApiKey);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                dynamic? data = JsonConvert.DeserializeObject(json);
                if (data?.cod == 200)
                {
                    return $"Thời tiết tại {city}:\n" +
                           $"Nhiệt độ: {data.main.temp}°C\n" +
                           $"Độ ẩm: {data.main.humidity}%\n" +
                           $"Mô tả: {data.weather[0].description}";
                }
                return $"Không tìm thấy thành phố {city}!";
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