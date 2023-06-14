using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AV;
using Microsoft.Win32;

namespace AntiVirus
{
    class RegistryScan
    {
        public static void Start_Rigistery_Timer()
        {
            Timer timer = new System.Timers.Timer();
            timer.Interval = 2000; // 20 seconds
            timer.Elapsed += ListProgramsInRunKey;
            timer.Start();
            while (true)
            {
                // Keep the thread alive
            }
        }
        private static void ListProgramsInRunKey(object sender, System.Timers.ElapsedEventArgs e)
        {
           
            const string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            int counter = 0;
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    foreach (string valueName in key.GetValueNames())
                    {
                        FileToScan fts = new FileToScan(key.GetValue(valueName)?.ToString(), $"suspicious file found at registery found at {key.GetValue(valueName)?.ToString()} - file rename");
                        AVEngine.QueueFileForScan(fts);
                        counter++;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
