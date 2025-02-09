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
            Thread AddClients = new Thread(() => ServerProgram.AddClients());
            AddClients.Start();

            Thread ControllClients = new Thread(() => ServerProgram.ControllClients());
            ControllClients.Start();
        }
        private static void AddClients()
        {
            StartBroadcast();
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(new IPEndPoint(IPAddress.Any, 20000));
            listenSocket.Listen();
            while (true)
            {
                Socket newClient = listenSocket.Accept();
                Guid name = Guid.NewGuid();
                Clients.TryAdd(name, newClient);
            }
        }
        private static void ControllClients()
        {
            string controllMessages = "";
            while (controllMessages != "END")
            {
                controllMessages = ReadLine();
                foreach (var sct in Clients.Values)
                {
                    byte[] requestBytes = Encoding.ASCII.GetBytes(controllMessages);
                    sct.Send(requestBytes);
                }
            }
        }
        private static void StartBroadcast()
        {
            UdpClient udpServ = new UdpClient();

            byte[] data = Encoding.ASCII.GetBytes("ServerHere");
            udpServ.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, 20000));
        }
    }
}