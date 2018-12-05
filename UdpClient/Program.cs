using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClient {
    internal class Program {
        private static void Main(string[] args) {
            var data = new byte[1024];
            string input, stringData;

            //构建TCP 服务器

            Console.WriteLine("This is a Client, host name is {0}", Dns.GetHostName());

            //设置服务IP，设置TCP端口号
            var ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);

            //定义网络类型，数据连接类型和网络协议UDP
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            var welcome = "Hello! ";
            data = Encoding.UTF8.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);
            server.ReceiveTimeout = 1500;
            var sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = sender;

            data = new byte[1024];
            //对于不存在的IP地址，加入此行代码后，可以在指定时间内解除阻塞模式限制
            //server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);
            var recv = server.ReceiveFrom(data, ref remote);
            Console.WriteLine("Message received from {0}: ", remote);
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
            while (true) {
                input = Console.ReadLine();
                if (input == "exit")
                    break;
                server.SendTo(Encoding.UTF8.GetBytes(input), remote);
                data = new byte [1024];
                try {
                    recv = server.ReceiveFrom(data, ref remote);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }

                stringData = Encoding.UTF8.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }

            Console.WriteLine("Stopping Client.");
            server.Close();
        }
    }
}