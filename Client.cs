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
            bool isOn = true;
            while (isOn)
            {
                bool Conn = false;
                Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string myIp = GetServerIp();
                ServerSocket.Connect(IPAddress.Parse(myIp), 20000);
                string Message = "";
                int Br = 0;
                byte[] buffer = new byte[1024];
                while (Message != "END")
                {
                    Br = ServerSocket.Receive(buffer);
                    Message = Encoding.ASCII.GetString(buffer, 0, Br);
                    WriteLine(Message);
                }
                ServerSocket.Close();
            }
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