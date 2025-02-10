using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ConsoleApp1
{
    internal class ClientSocket(string name, Socket MySocket, string Tag)
    {
        private string name { get; set; } = name;
        private Socket MySocket { get; set; } = MySocket;
        private string Tag { get; set; } = Tag;

        public byte[] Order(string command)
        {
            if (command == "END") { return new byte[0]; }
            byte[] requestBytes = Encoding.ASCII.GetBytes(command);
            MySocket.Send(requestBytes);

            byte[] answer = new byte[0];
            int Br = 1024;
            while (Br == 1024)
            {
                byte[] reader = new byte[1024];
                Br = MySocket.Receive(reader);
                byte[] bts = new byte[answer.Length + reader.Length];
                Buffer.BlockCopy(answer, 0, bts, 0, answer.Length);
                Buffer.BlockCopy(reader, 0, bts, answer.Length, reader.Length);
                answer = bts;
            }
            
            return answer;
        }

        public string Print() { return $"{name} : {MySocket} : {Tag}"; }
    }
}
