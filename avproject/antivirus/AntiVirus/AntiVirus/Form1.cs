using AV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AntiVirus
{
    public partial class Form1 : Form
    {
        AVEngine engine = new AVEngine();

        public Form1()
        {
            InitializeComponent();
            try
            {
                engine.Start();

            }
            catch (Exception ex)
            {

            }
        }

        private void btnscanfile_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
           // openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Select a text file";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                MessageBox.Show($"You selected file: {filePath}");

              


                AVEngine.QueueFileForScan(filePath);

            }

        }

        private void btnscandir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select a folder";
            folderBrowserDialog.ShowNewFolderButton = false;

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                Console.WriteLine($"You selected folder: {folderPath}");

                string[] files = Directory.GetFiles(folderPath);
                string[] subdirectoryEntries = Directory.GetDirectories(folderPath);

                if (subdirectoryEntries.Length != 0)
                {
                    foreach (string subdirectory in subdirectoryEntries)
                    {
                        string[] fileEntries = Directory.GetFiles(subdirectory);
                        foreach (string fileName in fileEntries)
                        {
                            AVEngine.QueueFileForScan(fileName);
                        }
                    }

                }
                foreach (string file in files)
                {
                    AVEngine.QueueFileForScan(file);
                }
            }
        }


    }
}
