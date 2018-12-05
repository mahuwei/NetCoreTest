using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpServer {
    internal class Program {
        private static void Main(string[] args) {
            int recv;
            var data = new byte[1024];

            //构建TCP 服务器

            //得到本机IP，设置TCP端口号         
            var ipep = new IPEndPoint(IPAddress.Any, 8001);
            var newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //绑定网络地址
            newsock.Bind(ipep);

            Console.WriteLine("This is a Server, host name is {0}", Dns.GetHostName());

            //等待客户机连接
            Console.WriteLine("Waiting for a client");

            //得到客户机IP
            var sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = sender;
            recv = newsock.ReceiveFrom(data, ref remote);
            Console.WriteLine("Message received from {0}: ", remote);
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));

            //客户机连接成功后，发送欢迎信息
            var welcome = "Welcome ! ";

            //字符串与字节数组相互转换
            data = Encoding.UTF8.GetBytes(welcome);

            //发送信息
            newsock.SendTo(data, data.Length, SocketFlags.None, remote);
            while (true) {
                data = new byte [1024];
                //发送接受信息
                recv = newsock.ReceiveFrom(data, ref remote);
                Console.WriteLine("{0} {1}", remote, Encoding.UTF8.GetString(data, 0, recv));
                newsock.SendTo(data, recv, SocketFlags.None, remote);
            }
        }
    }
}