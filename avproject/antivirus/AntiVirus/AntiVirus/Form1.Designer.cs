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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBoxExpert = new System.Windows.Forms.ListBox();
            this.expertCheckBox = new System.Windows.Forms.CheckBox();
            this.listBoxRegular = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnscanfile
            // 
            this.btnscanfile.Location = new System.Drawing.Point(171, 358);
            this.btnscanfile.Name = "btnscanfile";
            this.btnscanfile.Size = new System.Drawing.Size(75, 23);
            this.btnscanfile.TabIndex = 0;
            this.btnscanfile.Text = "scan file";
            this.btnscanfile.UseVisualStyleBackColor = true;
            this.btnscanfile.Click += new System.EventHandler(this.btnscanfile_Click);
            // 
            // btnscandir
            // 
            this.btnscandir.Location = new System.Drawing.Point(12, 358);
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Image = global::AntiVirus.Properties.Resources.ANTIVIRUS_LOGO;
            this.pictureBox1.Location = new System.Drawing.Point(12, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(351, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // listBoxExpert
            // 
            this.listBoxExpert.FormattingEnabled = true;
            this.listBoxExpert.HorizontalScrollbar = true;
            this.listBoxExpert.ItemHeight = 16;
            this.listBoxExpert.Items.AddRange(new object[] {
            "Log Info:"});
            this.listBoxExpert.Location = new System.Drawing.Point(12, 161);
            this.listBoxExpert.Name = "listBoxExpert";
            this.listBoxExpert.ScrollAlwaysVisible = true;
            this.listBoxExpert.Size = new System.Drawing.Size(583, 180);
            this.listBoxExpert.TabIndex = 5;
            this.listBoxExpert.Visible = false;
            // 
            // expertCheckBox
            // 
            this.expertCheckBox.AutoSize = true;
            this.expertCheckBox.Location = new System.Drawing.Point(12, 403);
            this.expertCheckBox.Name = "expertCheckBox";
            this.expertCheckBox.Size = new System.Drawing.Size(105, 20);
            this.expertCheckBox.TabIndex = 6;
            this.expertCheckBox.Text = "Expert Mode";
            this.expertCheckBox.UseVisualStyleBackColor = true;
            this.expertCheckBox.CheckedChanged += new System.EventHandler(this.expertCheckBox_CheckedChanged);
            // 
            // listBoxRegular
            // 
            this.listBoxRegular.FormattingEnabled = true;
            this.listBoxRegular.ItemHeight = 16;
            this.listBoxRegular.Items.AddRange(new object[] {
            "Log Info:"});
            this.listBoxRegular.Location = new System.Drawing.Point(12, 161);
            this.listBoxRegular.Name = "listBoxRegular";
            this.listBoxRegular.Size = new System.Drawing.Size(583, 180);
            this.listBoxRegular.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AntiVirus.Properties.Resources.media_1414973106656;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxRegular);
            this.Controls.Add(this.expertCheckBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblProgressbar);
            this.Controls.Add(this.btnscandir);
            this.Controls.Add(this.btnscanfile);
            this.Controls.Add(this.listBoxExpert);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ListBox listBoxExpert;
        private System.Windows.Forms.CheckBox expertCheckBox;
        public System.Windows.Forms.ListBox listBoxRegular;
    }
}

