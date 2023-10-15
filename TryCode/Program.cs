using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        // 定义服务器监听的IP地址和端口号
        const string ip = "127.0.0.1";
        const int port = 11000;

        // 创建IPEndPoint实例，表示特定的IP地址和端口号
        var localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        // 创建Socket实例，用于监听客户端的连接请求
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // 将Socket绑定到本地端点，准备接收连接
            listener.Bind(localEndPoint);

            // 设置Socket为监听模式，并定义最大的等待连接队列长度
            listener.Listen(10);

            Console.WriteLine($"Server is listening on {ip}:{port}...");

            // 无限循环，持续等待并处理客户端的连接请求
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");

                // 当有客户端请求连接时，接受连接并返回一个新的Socket，用于与客户端通信
                Socket handler = listener.Accept();
                Console.WriteLine($"Connected to {handler.RemoteEndPoint}");

                string data = null;

                // 持续读取从客户端接收到的数据
                while (true)
                {
                    // 定义byte数组，用于存储接收到的数据
                    byte[] bytes = new byte[1024];

                    // 读取数据并存储到byte数组中，返回读取的字节数量
                    int byteCount = handler.Receive(bytes);

                    // 如果没有接收到数据，即客户端已关闭连接，则跳出循环
                    if (byteCount <= 0) break;

                    // 将接收到的字节转换为字符串，并输出到控制台
                    data += Encoding.UTF8.GetString(bytes, 0, byteCount);
                    Console.WriteLine($"Received: {data}");

                    // 将接收到的数据回传给客户端，实现echo服务
                    handler.Send(bytes, 0, byteCount, SocketFlags.None);
                }

                // 关闭与客户端的连接
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e)
        {
            // 输出异常信息到控制台
            Console.WriteLine(e.ToString());
        }
    }
}
