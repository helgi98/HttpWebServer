namespace Tray_Version1._0
{
    partial class SuccessfullLogin
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
            this.WelcomeAdmin = new System.Windows.Forms.Label();
            this.Continue = new System.Windows.Forms.Button();
            this.WelcomePicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.WelcomePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // WelcomeAdmin
            // 
            this.WelcomeAdmin.AutoSize = true;
            this.WelcomeAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeAdmin.Location = new System.Drawing.Point(269, 71);
            this.WelcomeAdmin.Name = "WelcomeAdmin";
            this.WelcomeAdmin.Size = new System.Drawing.Size(85, 29);
            this.WelcomeAdmin.TabIndex = 0;
            this.WelcomeAdmin.Text = "label1";
            // 
            // Continue
            // 
            this.Continue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Continue.Location = new System.Drawing.Point(265, 353);
            this.Continue.Name = "Continue";
            this.Continue.Size = new System.Drawing.Size(140, 98);
            this.Continue.TabIndex = 2;
            this.Continue.Text = "Continue";
            this.Continue.UseVisualStyleBackColor = true;
            this.Continue.Click += new System.EventHandler(this.Continue_Click);
            // 
            // WelcomePicture
            // 
            this.WelcomePicture.Image = global::Tray_Version1._0.Properties.Resources.Welcome_PNG_Pic;
            this.WelcomePicture.Location = new System.Drawing.Point(122, 133);
            this.WelcomePicture.Name = "WelcomePicture";
            this.WelcomePicture.Size = new System.Drawing.Size(450, 200);
            this.WelcomePicture.TabIndex = 3;
            this.WelcomePicture.TabStop = false;
            // 
            // SuccessfulLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(691, 463);
            this.Controls.Add(this.WelcomePicture);
            this.Controls.Add(this.Continue);
            this.Controls.Add(this.WelcomeAdmin);
            this.Name = "SuccessfulLogin";
            this.Text = "SuccessfulLogin";
            ((System.ComponentModel.ISupportInitialize)(this.WelcomePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WelcomeAdmin;
        private System.Windows.Forms.Button Continue;
        private System.Windows.Forms.PictureBox WelcomePicture;
    }
}