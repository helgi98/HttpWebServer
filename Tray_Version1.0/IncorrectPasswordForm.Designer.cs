namespace Tray_Version1._0
{
    partial class IncorrectPasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncorrectPasswordForm));
            this.WrongPasswordLabel = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.TryAgain = new System.Windows.Forms.Button();
            this.Picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // WrongPasswordLabel
            // 
            this.WrongPasswordLabel.AutoSize = true;
            this.WrongPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WrongPasswordLabel.Location = new System.Drawing.Point(204, 80);
            this.WrongPasswordLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.WrongPasswordLabel.Name = "WrongPasswordLabel";
            this.WrongPasswordLabel.Size = new System.Drawing.Size(337, 25);
            this.WrongPasswordLabel.TabIndex = 0;
            this.WrongPasswordLabel.Text = "Wrong Username or Password!";
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(132, 359);
            this.Exit.Margin = new System.Windows.Forms.Padding(5);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(125, 97);
            this.Exit.TabIndex = 1;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.button1_Click);
            // 
            // TryAgain
            // 
            this.TryAgain.Location = new System.Drawing.Point(492, 359);
            this.TryAgain.Name = "TryAgain";
            this.TryAgain.Size = new System.Drawing.Size(117, 97);
            this.TryAgain.TabIndex = 2;
            this.TryAgain.Text = "Try again";
            this.TryAgain.UseVisualStyleBackColor = true;
            this.TryAgain.Click += new System.EventHandler(this.TryAgain_Click);
            // 
            // Picture
            // 
            this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
            this.Picture.Location = new System.Drawing.Point(248, 158);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(304, 168);
            this.Picture.TabIndex = 3;
            this.Picture.TabStop = false;
            // 
            // IncorrectPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(782, 543);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.TryAgain);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.WrongPasswordLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "IncorrectPasswordForm";
            this.Text = "ERROR!";
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WrongPasswordLabel;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button TryAgain;
        private System.Windows.Forms.PictureBox Picture;
    }
}