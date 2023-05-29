using AntiVirus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AV
{
    public class AVEngine
    {
        private static readonly List<string> DirectoryToWatchList = new List<string> 
        {
            @"C:\Users\majd4\AppData",
            @"C:\Users\majd4\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup",
        };
          
      
        


        public static Queue<string> FilesToScan = new Queue<string>();
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

            foreach(string dir in DirectoryToWatchList) 
            {
                Thread appDataWatcherThread = new Thread(() => DirectoryWatcher.InitWatcher(dir));
                appDataWatcherThread.Start();
            }

            Thread processMonitor = new Thread(ProccessMonitor.InitProcessMonitor);
            processMonitor.Start();

            Thread portListenerThred = new Thread(PortListener.InitPortListener);
            portListenerThred.Start();
        }

        public static void QueueFileForScan(string filename)
        {
            lock (FilesToScan)
            {
                FilesToScan.Enqueue(filename);
            }
        }

        private void ScannerThread()
        {
            while (true) // Scanner thread loops until the program exits
            {
                string fileToScan = null; // To hold what we take out of the queue
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

                if (fileToScan != null)
                {
                    // Now, scan the file
                    int scanerResult = FileScanner.Scan(fileToScan);
                    if (scanerResult == -1)
                    {
                       
                        using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                        {
                            // Write an initial message or header
                            writer.WriteLine(DateTime.Now + " WARNING! A VIRUS WAS DETECTED at path:" + fileToScan);
                        }
                        //Console.WriteLine("A VIRUS WAS DETECTED");
                    }
                    else
                        if (scanerResult == 1)
                        {
                        //using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                        //{
                        //    // Write an initial message or header
                        //    writer.WriteLine(DateTime.Now + "A VIRUS WAS DETECTED at path:" + fileToScan);
                        //}
                    }
                        else
                        {
                        using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                        {
                            // Write an initial message or header
                            writer.WriteLine(DateTime.Now + " WARNING! AN UNKNOWN FILE WAS DETECTED at path:" + fileToScan);
                        }
                    }
                }
            }
        }
    }
}
