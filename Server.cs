using ConsoleApp1;
using ConsoleApp1.Client;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Console;

namespace CharTest_csharp
{
    internal class ServerProgram
    {
        private static List<ClientSocket> Clients = new List<ClientSocket>(100);
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
                Clients.Add(new ClientSocket(Clients.Count.ToString(), newClient, ""));
            }
        }
        private static void ControllClients()
        {
            string controllMessages = "";
            while (controllMessages != "END")
            {
                controllMessages = ReadLine();
                foreach (var sct in Clients)
                {
                    var a = sct.Order(controllMessages);
                    if (controllMessages == "takeScreen")
                    {
                        File.WriteAllBytes("C:\\Users\\aleks\\Saved Games\\C++C#Projects\\debug_received_server.png", a);
                    }
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