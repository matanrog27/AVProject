using AV;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("Your Application Name", Application.ExecutablePath);
            Console.WriteLine("Hello, World!");
            FileScanner fs = new FileScanner();
            fs.GenerateLists();

            Application.Run(new Form1());
            //AVEngine engine = new AVEngine();

            //engine.Start();

            //Console.WriteLine("Queueing file...");
            //engine.QueueFileForScan(@"C:\Users\ruppin\Desktop\VIRUS.txt");

            //Console.WriteLine("Queued the first file.");
            //Thread.Sleep(5000);

            //engine.QueueFileForScan(@"C:\Users\ruppin\Desktop\NOT_VIRUS.txt");
            //Console.WriteLine("Queued the second file.");

            //while (true)
            //{
            //    lock (engine.BadFiles)
            //    {
            //        if (engine.BadFiles.Count > 0)
            //        {
            //            Console.WriteLine("Bad File caught " + engine.BadFiles.Dequeue());
            //        }
            //    }
            //}

            //Console.WriteLine("Done");
        }

        private static void AddToRunRegistry()
        {
            const string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            const string keyName = "MyAntivirus";

            RegistryKey runKey = Registry.CurrentUser.OpenSubKey(keyPath, true);

            if (runKey.GetValue(keyName) == null)
            {
                runKey.SetValue(keyName, AppDomain.CurrentDomain.BaseDirectory + "MyAntivirus.exe");
            }

            runKey.Close();
        }
    }
}
