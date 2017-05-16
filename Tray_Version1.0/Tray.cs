using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tray_Version1._0.Properties;

namespace Tray_Version1._0
{
    public partial class Tray : Form
    {
        private NotifyIcon icon;
        private ContextMenu menu;
        public static bool userPortIsOpen = false;

        public Tray(string AdminName)
        {
            InitializeComponent();
            icon = new NotifyIcon();
            menu = new ContextMenu();

            menu.MenuItems.Add("Port is now closed");
            menu.MenuItems.Add("Open user port", onOpen);            
            menu.MenuItems.Add("Exit", onExit);

            icon.Icon = Resources.Koala;
            icon.ContextMenu = menu;                      
            icon.Visible = true;
            icon.Text = "Welcome, " + AdminName + '!';
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        private void onExit(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Application.Exit();
        }

        private void onOpen(object sender, EventArgs e)
        {
            if (!userPortIsOpen)
            {
                userPortIsOpen = true;

                menu.MenuItems.Clear();
                menu.MenuItems.Add("Port is now opened");
                menu.MenuItems.Add("Close user port", onClose);
                menu.MenuItems.Add("Exit", onExit);

                HttpWebServer.HttpServer.GetServer().RunApplication(FileManagerApp.FileManagerApp.GetApp().Name, 8080);
            }            
        }

        private void onClose(object sender, EventArgs e)
        {
            if (userPortIsOpen)
            {
                userPortIsOpen = false;
                menu.MenuItems.Clear();
                menu.MenuItems.Add("Port is now closed");
                menu.MenuItems.Add("Open user port", onOpen);
                menu.MenuItems.Add("Exit", onExit);

                HttpWebServer.HttpServer.GetServer().StopApplication(FileManagerApp.FileManagerApp.GetApp().Name);
            }
        }

    }
}
