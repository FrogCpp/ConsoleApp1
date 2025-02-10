using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;
using ConsoleApp1.Client;


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
                    byte[] ans = ExecuteCommand(Message);
                    if (ans.Length == 0) { isOn = false; break; }

                    ServerSocket.Send(ans);
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
            return remoteEndPoint.Address.ToString();
        }

        private static byte[] ExecuteCommand(string command)
        {
            if (command == "Off")
            {
                return new byte[0];
            }
            else if (command == "takeScreen")
            {
                var a = Screenshot.TakeScreenshot();
                File.WriteAllBytes("C:\\Users\\aleks\\Saved Games\\C++C#Projects\\debug_received_client.png", a);
                return a;
            }
            else
            {
                WriteLine(command);
            }
            return new byte[1];
        }
    }
}