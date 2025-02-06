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
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork,
                                             SocketType.Stream,
                                             ProtocolType.Tcp);


            ServerSocket.Connect(IPAddress.Parse("127.0.0.1"), 20000);

            WriteLine("Start");
            string Message = "None";
            do
            {
                Message = ReadLine();
                byte[] requestBytes = Encoding.ASCII.GetBytes(Message);
                ServerSocket.Send(requestBytes);
            } while (Message != "END");
            ServerSocket.Close();
        }
    }
}