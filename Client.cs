using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace CharTest_csharp
{
    internal class ClientProgram
    {
        private static void Connection(Socket ServerSocket)
        {
            WriteLine("Start");
            string Message = "None";
            while (Message != "END")
            {
                Message = ReadLine();
                byte[] requestBytes = Encoding.ASCII.GetBytes(Message);
                int bytesSent = 0;
                bytesSent += ServerSocket.Send(requestBytes, bytesSent, requestBytes.Length - bytesSent, SocketFlags.None);
            }
            ServerSocket.Close();
        }
        public void Client()
        {
            WriteLine("Client");
            Socket ServSocket = new Socket(AddressFamily.InterNetwork,
                                             SocketType.Stream,
                                             ProtocolType.Tcp);


            ServSocket.Connect(IPAddress.Parse("127.0.0.1"), 20000);

            Connection(ServSocket);


            //byte[] requestBytes = Encoding.ASCII.GetBytes(@$"Hello World!");

            //int bytesSent = 0;
            //bytesSent += ServSocket.Send(requestBytes, bytesSent, requestBytes.Length - bytesSent, SocketFlags.None);

            //byte[] buffer = new byte[1024];
            //int Br = ServSocket.Receive(buffer);
            //WriteLine(Encoding.ASCII.GetString(buffer, 0, Br));
            //Read();
        }
    }
}