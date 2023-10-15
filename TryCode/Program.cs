using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        // ���������������IP��ַ�Ͷ˿ں�
        const string ip = "127.0.0.1";
        const int port = 11000;

        // ����IPEndPointʵ������ʾ�ض���IP��ַ�Ͷ˿ں�
        var localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        // ����Socketʵ�������ڼ����ͻ��˵���������
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // ��Socket�󶨵����ض˵㣬׼����������
            listener.Bind(localEndPoint);

            // ����SocketΪ����ģʽ�����������ĵȴ����Ӷ��г���
            listener.Listen(10);

            Console.WriteLine($"Server is listening on {ip}:{port}...");

            // ����ѭ���������ȴ�������ͻ��˵���������
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");

                // ���пͻ�����������ʱ���������Ӳ�����һ���µ�Socket��������ͻ���ͨ��
                Socket handler = listener.Accept();
                Console.WriteLine($"Connected to {handler.RemoteEndPoint}");

                string data = null;

                // ������ȡ�ӿͻ��˽��յ�������
                while (true)
                {
                    // ����byte���飬���ڴ洢���յ�������
                    byte[] bytes = new byte[1024];

                    // ��ȡ���ݲ��洢��byte�����У����ض�ȡ���ֽ�����
                    int byteCount = handler.Receive(bytes);

                    // ���û�н��յ����ݣ����ͻ����ѹر����ӣ�������ѭ��
                    if (byteCount <= 0) break;

                    // �����յ����ֽ�ת��Ϊ�ַ����������������̨
                    data += Encoding.UTF8.GetString(bytes, 0, byteCount);
                    Console.WriteLine($"Received: {data}");

                    // �����յ������ݻش����ͻ��ˣ�ʵ��echo����
                    handler.Send(bytes, 0, byteCount, SocketFlags.None);
                }

                // �ر���ͻ��˵�����
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e)
        {
            // ����쳣��Ϣ������̨
            Console.WriteLine(e.ToString());
        }
    }
}
