using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Console;

namespace CharTest_csharp
{
    internal class ServerProgram
    {
        private static ConcurrentDictionary<Guid, Socket> Clients = new ConcurrentDictionary<Guid, Socket> ();
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

            WriteLine("Start");

            while (true)
            {
                Socket newClient = listenSocket.Accept();
                Guid name = Guid.NewGuid();

                Clients.TryAdd(name, newClient);


                Thread clientThread = new Thread(() => WorkWithClient(name, newClient));
                clientThread.Start();
            }
        }

        private static void WorkWithClient(Guid name, Socket ClientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int Br = 0;
                while (Encoding.ASCII.GetString(buffer, 0, Br) != "END")
                {
                    Br = ClientSocket.Receive(buffer);
                    Write(Encoding.ASCII.GetString(buffer, 0, Br) + '\n');
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
            finally
            {
                ClientSocket.Close();
                Clients.TryRemove(name, out _);
            }
        }
    }
}