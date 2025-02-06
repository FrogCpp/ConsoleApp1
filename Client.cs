using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace CharTest_csharp
{
    internal class ClientProgram
    {
        public void Client()
        {
            WriteLine("Client");
            GetServerIp();
            WriteLine("Start");
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork,
                                             SocketType.Stream,
                                             ProtocolType.Tcp);

            bool Conn = false;
            while (!Conn)
            {
                try
                {
                    ServerSocket.Connect(IPAddress.Parse("127.0.0.1"), 20000);
                    Conn = true;
                }
                catch (Exception e) 
                {
                    Conn = false;
                }
                finally
                {
                    if (Conn) { WriteLine("aa"); }
                }
            }

            string Message = "None";
            do
            {
                Message = ReadLine();
                byte[] requestBytes = Encoding.ASCII.GetBytes(Message);
                ServerSocket.Send(requestBytes);
            } while (Message != "END");
            ServerSocket.Close();
        }

        private static string GetServerIp()
        {
            string message = "None";
            IPEndPoint remoteEndPoint = null;
            UdpClient udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(
                SocketOptionLevel.Socket,
                SocketOptionName.ReuseAddress,
                true
            );
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 20000));
            while (message != "ServerHere")
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                message = Encoding.ASCII.GetString(data);
            }
            WriteLine(remoteEndPoint.Address.ToString());
            return remoteEndPoint.Address.ToString();
        }
    }
}