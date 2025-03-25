// Client/Program.cs
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClient
{
    public class WeatherClient
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 8888;

        public async Task GetWeatherAsync()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(ServerIp, Port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        Console.Write("Nhập tên thành phố: ");
                       string city = Console.ReadLine() ?? string.Empty;

                       byte[] request = Encoding.UTF8.GetBytes(city ?? string.Empty);
                        await stream.WriteAsync(request, 0, request.Length);

                        byte[] buffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        Console.WriteLine(response);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
    }

    class Program
    {
        static async Task Main()
        {
            WeatherClient client = new WeatherClient();
            await client.GetWeatherAsync();
        }
    }
}