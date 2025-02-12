﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using static System.Console;

namespace CharTest_csharp
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            try
            {
                string rg = ReadLine();
                if (rg == "C")
                {
                    var a = new ClientProgram();
                    a.Client();
                }
                else if (rg == "S")
                {
                    var a = new ServerProgram();
                    a.Server();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}