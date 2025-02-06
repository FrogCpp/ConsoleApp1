using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace CharTest_csharp
{
    internal class ServerProgram
    {
        private static void Connection(Socket ClientSocket)
        {
            WriteLine("Start");
            byte[] buffer = new byte[1024];
            int Br = 0;
            while (Encoding.ASCII.GetString(buffer, 0, Br) != "END")
            {
                Br = ClientSocket.Receive(buffer);
                Write(Encoding.ASCII.GetString(buffer, 0, Br) + '\n');
            }
            ClientSocket.Close();
        }
        public void Server()
        {
            WriteLine("Server");
            Socket listenSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp);

            var a = IPAddress.Any;
            listenSocket.Bind(new IPEndPoint(a, 20000));

            //start listening
            listenSocket.Listen();

            Connection(listenSocket.Accept());



            //var ls = listenSocket.Accept();


            //byte[] buffer = new byte[1024];
            //int Br = ls.Receive(buffer);
            //WriteLine(Encoding.ASCII.GetString(buffer, 0, Br));

            //byte[] requestBytes = Encoding.ASCII.GetBytes(@$"Complite");

            //int bytesSent = 0;
            //bytesSent += ls.Send(requestBytes, bytesSent, requestBytes.Length - bytesSent, SocketFlags.None);
            //Read();
            //ls.Close();
            //listenSocket.Close();
        }
    }
}