using AntiVirus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;
namespace AV
{
    public class AVEngine
    {
        private static readonly List<string> DirectoryToWatchList = new List<string> 
        {
            @"C:\Users\mikie\AppData",
            @"C:\Users\mikie\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup",
        };
   
        public static Queue<FileToScan> FilesToScan = new Queue< FileToScan>();
        public Queue<string> BadFiles = new Queue<string>();

        public void Start()
        {
            
            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
            {
                // Write an initial message or header
                writer.WriteLine(DateTime.Now + " av started running");
            }
            //DirectoryWatcher dw = new DirectoryWatcher();
            Thread threadscanner = new Thread(ScannerThread);
            threadscanner.Start();
            Thread registeryThread = new Thread(RegistryScan.Start_Rigistery_Timer);
            registeryThread.Start();
            foreach (string dir in DirectoryToWatchList) 
            {
                Thread appDataWatcherThread = new Thread(() => DirectoryWatcher.InitWatcher(dir));
                appDataWatcherThread.Start();
            }

            Thread processMonitor = new Thread(ProccessMonitor.InitProcessMonitor);
            processMonitor.Start();

            Thread portListenerThred = new Thread(PortListener.InitPortListener);
            portListenerThred.Start();
        }

        public static void QueueFileForScan(FileToScan fts)
        {
            lock (FilesToScan)
            {
                FilesToScan.Enqueue(fts);
            }
        }

        private void ScannerThread()
        {
            while (true) // Scanner thread loops until the program exits
            {
                FileToScan fileToScan = null; // To hold what we take out of the queue
                lock (FilesToScan)
                {
                    try
                    {
                        fileToScan = FilesToScan.Dequeue();
                    }
                    catch (Exception ex)
                    {
                        // Nothing in the queue
                    }
                }

                if ((fileToScan?.file_path!=string.Empty) && (fileToScan?.file_path!=null) && fileToScan!=null)
                {
                    // Now, scan the file
                    int scanerResult = FileScanner.Scan(fileToScan.file_path);
                    if (scanerResult == 1)
                    {
                        //white list file
                       
                    }
                    else if (scanerResult == -1)
                    {
                            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                            {
                                // Write an to log
                                writer.WriteLine(DateTime.Now + " WARNING! A VIRUS WAS DETECTED - {0} at path:{1}",fileToScan.reson_for_scan,fileToScan.file_path );
                                MessageBox.Show("Danger! This is a dangerous virus!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                    }
                    else if (scanerResult == -2)
                    {
                        using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                        {
                            // Write to log
                            writer.WriteLine(DateTime.Now + " WARNING! A VIRUS WAS DETECTED - {0} by huristic signature at path:{1}", fileToScan.reson_for_scan, fileToScan.file_path);
                            MessageBox.Show("Danger! This is a dangerous virus!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                            {
                                // Write an initial message or header
                                writer.WriteLine(DateTime.Now + " WARNING! AN UNKNOWN FILE WAS DETECTED - {0} at path:{1}", fileToScan.reson_for_scan, fileToScan.file_path);
                            }
                    }
                }
            }
        }
    }
}
