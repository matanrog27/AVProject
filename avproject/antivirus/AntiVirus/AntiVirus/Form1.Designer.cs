using System;

namespace AntiVirus
{
    partial class Form1
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
            this.btnscanfile = new System.Windows.Forms.Button();
            this.btnscandir = new System.Windows.Forms.Button();
            this.lblProgressbar = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnscanfile
            // 
            this.btnscanfile.Location = new System.Drawing.Point(22, 36);
            this.btnscanfile.Name = "btnscanfile";
            this.btnscanfile.Size = new System.Drawing.Size(75, 23);
            this.btnscanfile.TabIndex = 0;
            this.btnscanfile.Text = "scan file";
            this.btnscanfile.UseVisualStyleBackColor = true;
            this.btnscanfile.Click += new System.EventHandler(this.btnscanfile_Click);
            // 
            // btnscandir
            // 
            this.btnscandir.Location = new System.Drawing.Point(22, 94);
            this.btnscandir.Name = "btnscandir";
            this.btnscandir.Size = new System.Drawing.Size(115, 23);
            this.btnscandir.TabIndex = 1;
            this.btnscandir.Text = "scan directory";
            this.btnscandir.UseVisualStyleBackColor = true;
            this.btnscandir.Click += new System.EventHandler(this.btnscandir_Click);
            // 
            // lblProgressbar
            // 
            this.lblProgressbar.AutoSize = true;
            this.lblProgressbar.Location = new System.Drawing.Point(92, 266);
            this.lblProgressbar.Name = "lblProgressbar";
            this.lblProgressbar.Size = new System.Drawing.Size(0, 16);
            this.lblProgressbar.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 163);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblProgressbar);
            this.Controls.Add(this.btnscandir);
            this.Controls.Add(this.btnscanfile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button btnscanfile;
        private System.Windows.Forms.Button btnscandir;
        private System.Windows.Forms.Label lblProgressbar;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

