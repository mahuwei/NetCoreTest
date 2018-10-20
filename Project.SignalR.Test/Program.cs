using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Project.SignalR.Test {
    internal class Program {
        private static HubConnection _connection;

        private static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub")
                .Build();

            _connection.Closed += async error => {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} 连接关闭（Closed）。");
                //await Task.Delay(new Random().Next(0, 5) * 1000);
                //await _connection.StartAsync();
            };


            _connection.On<string, string>("ReceiveMessage",
                (user, message) => {
                    Console.WriteLine(
                        $"{DateTime.Now:HH:mm:ss} ReceiveMessage,user:{user} message:{message}");
                });

            StartConnect();

            do {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                if (input.ToLower() == "exit") {
                    CloseConnection();
                    break;
                }

                SendMessage(input);
            } while (true);

            Console.WriteLine("按任意键，关闭应用。");
            Console.ReadKey();
        }

        private static async void StartConnect() {
            try {
                await _connection.StartAsync();
                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss} StartConnect()成功，{_connection}");
            }
            catch (Exception e) {
                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss} StartConnect，报错：{e.Message}");
            }
        }

        private static async void SendMessage(string input) {
            try {
                await _connection.InvokeAsync("SendMessage", "mhw", input);
            }
            catch (Exception e) {
                Console.WriteLine(
                    $"{DateTime.Now:HH:mm:ss} SendMessage，报错：{e.Message}");
            }
        }

        private static async void CloseConnection() {
            await _connection.StopAsync();
        }
    }
}