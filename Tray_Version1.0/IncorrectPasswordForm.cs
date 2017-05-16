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
    public partial class IncorrectPasswordForm : Form
    {
        public IncorrectPasswordForm()
        {
            InitializeComponent();
            Size = new Size(700, 500);

            Exit.Size = new Size(150, 100);
            TryAgain.Size = new Size(150, 100);

            WrongPasswordLabel.Left = (this.ClientSize.Width - WrongPasswordLabel.Size.Width) / 2; 
            Picture.Left = (this.ClientSize.Width - Picture.Size.Width) / 2;

            WrongPasswordLabel.Top = 75;
            Picture.Top = (this.ClientSize.Height - Picture.Size.Height) / 2;

            Exit.Top = this.ClientSize.Height - 125;
            TryAgain.Top = this.ClientSize.Height - 125;

            Exit.Left = 125;
            TryAgain.Left = this.ClientSize.Width - 275;

            StartPosition = FormStartPosition.CenterScreen;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Visible = false;           
            Application.Exit();
        }

        private void TryAgain_Click(object sender, EventArgs e)
        {
            Hide();
            Visible = false; 
            AdminView adm = new AdminView();
            adm.Show();
        }
    }
}
