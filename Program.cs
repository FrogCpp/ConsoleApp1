using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace CharTest_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string param = ReadLine();
            Write(param);
            if (param == "10")
            {
                WriteLine("s");
                Socket listenSocket = new Socket(AddressFamily.InterNetwork,
                                                 SocketType.Stream,
                                                 ProtocolType.Tcp);

                var a = IPAddress.Any;
                Write(a);
                listenSocket.Bind(new IPEndPoint(a, 20000));

                //start listening
                listenSocket.Listen();
                listenSocket.Accept();

                Write("Server");

                byte[] buffer = new byte[1024];
                while (true)
                {
                    int Br = listenSocket.Receive(buffer);

                    if (Br == 0) break;
                }
                Write(buffer);
                Read();
            }
            else
            {
                WriteLine("c");
                var cl = new CodeFile1();
                cl.mainTwo();
            }
        }
    }
}