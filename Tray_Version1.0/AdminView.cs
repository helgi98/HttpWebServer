using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using FileManagerApp;

namespace Tray_Version1._0
{
    public partial class AdminView : Form
    {
        public AdminView()
        {
            InitializeComponent();
            Size = new Size(800, 600);
            Login.Size = new Size(150, 100);
            Login.Location = new Point(300, 400);
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Visible = false;
            Application.Exit();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Hide();
            if (PasswordManager.check(textBoxName.Text.Trim(), textBoxPassword.Text.Trim()))
            {
                SuccessfullLogin success = new SuccessfullLogin(textBoxName.Text);
                success.Show();               
            }
            else
            {                
                IncorrectPasswordForm incorrect = new IncorrectPasswordForm();
                incorrect.Show();                
            }
        }
    }
}
