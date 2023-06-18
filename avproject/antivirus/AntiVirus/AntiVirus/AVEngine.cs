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
        private static readonly object queueLock = new object(); // Lock object for the queue
        private static readonly object logFileLock = new object(); // Lock object for the log file

        public void Start(Form1 form)
        {

            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
            {
                // Write an initial message or header
                writer.WriteLine(DateTime.Now + " av started running");
            }

            //DirectoryWatcher dw = new DirectoryWatcher();
            Thread threadscanner = new Thread(() => ScannerThread(form));
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
            lock (queueLock)
            {
                FilesToScan.Enqueue(fts);
            }
        }

        private void ScannerThread(Form1 form)
        {
            while (true) // Scanner thread loops until the program exits
            {
                FileToScan fileToScan = null; // To hold what we take out of the queue
                lock (queueLock)
                {
                    if (FilesToScan.Count > 0)
                    {
                        fileToScan = FilesToScan.Dequeue();
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
                        lock (logFileLock)
                        {
                            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                            {
                                string logExpertMessage = $"-{DateTime.Now} WARNING! A VIRUS WAS DETECTED - {fileToScan.reson_for_scan} at path: {fileToScan.file_path}\n\n";
                                string logMessage = $"-WARNING! A VIRUS WAS DETECTED, at path {fileToScan.file_path}";
                                form.Invoke(new Action(() =>
                                {
                                    form.listBoxExpert.Items.Add(logExpertMessage);
                                    form.listBoxRegular.Items.Add(logMessage);
                                    form.listBoxRegular.Refresh();
                                    form.listBoxExpert.Refresh();
                                }));
                                writer.WriteLine(DateTime.Now + " WARNING! A VIRUS WAS DETECTED - {0} at path:{1}", fileToScan.reson_for_scan, fileToScan.file_path);
                                MessageBox.Show("Danger! This is a dangerous virus!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else if (scanerResult == -2)
                    {
                        lock (logFileLock)
                        {
                            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                            {
                                string logExpertMessage = $"-{DateTime.Now} WARNING! A VIRUS WAS DETECTED - {fileToScan.reson_for_scan} by huristic signature at path: {fileToScan.file_path}";
                                string logMessage = "-WARNING! A VIRUS WAS DETECTED";
                                form.Invoke(new Action(() =>
                                {
                                    form.listBoxExpert.Items.Add(logExpertMessage);
                                    form.listBoxRegular.Items.Add(logMessage);
                                    form.listBoxRegular.Refresh();
                                    form.listBoxExpert.Refresh();
                                }));
                                
                                writer.WriteLine(DateTime.Now + " WARNING! A VIRUS WAS DETECTED - {0} by huristic signature at path:{1}", fileToScan.reson_for_scan, fileToScan.file_path);
                                MessageBox.Show("Danger! This is a dangerous virus!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else if(scanerResult == 0)
                    {
                        lock (logFileLock)
                        {
                            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                            {
                                string logExpertMessage = $"-{DateTime.Now} WARNING! AN UNKNOWN FILE WAS DETECTED - {fileToScan.reson_for_scan} at path: {fileToScan.file_path}";
                                string logMessage = "-WARNING! UNKNOWN FILE DETECTED";
                                form.Invoke(new Action(() =>
                                {
                                    form.listBoxExpert.Items.Add(logExpertMessage);
                                    form.listBoxRegular.Items.Add(logMessage);
                                    form.listBoxRegular.Refresh();
                                    form.listBoxExpert.Refresh();
                                }));
                                writer.WriteLine(DateTime.Now + " WARNING! AN UNKNOWN FILE WAS DETECTED - {0} at path:{1}", fileToScan.reson_for_scan, fileToScan.file_path);
                            }
                        }
                    }
                    else
                    {
                        lock (logFileLock)
                        {
                            using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                            {
                                string logExpertMessage = $"-{DateTime.Now} FAILED TO SCAN FILE!";
                                string logMessage = "-FAILED TO SCAN FILE!";
                                form.Invoke(new Action(() =>
                                {
                                    form.listBoxExpert.Items.Add(logExpertMessage);
                                    form.listBoxRegular.Items.Add(logMessage);
                                    form.listBoxRegular.Refresh();
                                    form.listBoxExpert.Refresh();
                                }));
                                writer.WriteLine(DateTime.Now + $" failed to scan file:");
                            }
                        }
                    }
                }
            }
        }
    }
}
