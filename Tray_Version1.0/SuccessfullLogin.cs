using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tray_Version1._0
{
    public partial class SuccessfullLogin : Form
    {
        private string AdminName;

        public SuccessfullLogin(string name)
        {
            AdminName = name;
            InitializeComponent();
            Size = new Size(700, 500);
            WelcomeAdmin.Text = "Greetings, " + AdminName + '!';
            WelcomeAdmin.Left = (this.ClientSize.Width - WelcomeAdmin.Size.Width) / 2;
            WelcomeAdmin.Top = 50;

            WelcomePicture.Left = (this.ClientSize.Width - WelcomePicture.Size.Width) / 2;
            WelcomePicture.Top = (this.ClientSize.Height - WelcomePicture.Size.Height) / 2 - 25;

            Continue.Size = new Size(150, 100);
            Continue.Left = (this.ClientSize.Width - Continue.Size.Width) / 2;
            Continue.Top = (this.ClientSize.Height - Continue.Size.Height) - 25;
           
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            Hide();
            Tray tray = new Tray(AdminName);
            tray.Show();
        }
    }
}
