using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace CharTest_csharp
{
    internal class CodeFile1
    {
        public void mainTwo()
        {
            Socket senderSocket = new Socket(AddressFamily.InterNetwork,
                                             SocketType.Stream,
                                             ProtocolType.Tcp);


            senderSocket.Connect(IPAddress.Any, 4001);

            Write("Client");

            byte[] requestBytes = Encoding.ASCII.GetBytes(@$"Hello World!");

            Write(requestBytes);

            int bytesSent = 0;
            while (bytesSent < requestBytes.Length)
            {
                bytesSent += senderSocket.Send(requestBytes, bytesSent, requestBytes.Length - bytesSent, SocketFlags.None);
            }
        }
    }
}