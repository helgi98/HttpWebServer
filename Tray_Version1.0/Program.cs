using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Tray_Version1._0
{
    static class Program
    {        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            HttpWebServer.HttpServer.GetServer().AddApplication(FileManagerApp.FileManagerApp.GetApp());
            StreamReader reader = new StreamReader("Passwords.txt");
            if ((reader.ReadLine() == null))
            {
                reader.Close();
                PasswordManager.GeneratePasswords();
            }        
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AdminView());
        }        
    }
}
