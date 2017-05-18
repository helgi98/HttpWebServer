using HttpWebServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer.GetServer().AddApplication(FileManagerApp.GetApp());
            HttpServer.GetServer().RunApplication(FileManagerApp.GetApp().Name, 8080);
            if (File.Exists("Passwords.txt"))
            {
                StreamReader reader = new StreamReader("Passwords.txt");
                if ((reader.ReadLine() == null))
                {
                    reader.Close();
                    PasswordManager.GeneratePasswords();
                }
            }
            else
                PasswordManager.GeneratePasswords();

            Console.ReadLine();
        }
    }
}
