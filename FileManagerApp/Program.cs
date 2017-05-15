﻿using HttpWebServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer.GetServer().AddApplication("user");
            HttpServer.GetServer().RunApplication("user", 8080);

            Console.ReadLine();
        }
    }
}