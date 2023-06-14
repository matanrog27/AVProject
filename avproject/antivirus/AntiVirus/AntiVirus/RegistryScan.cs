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
        private static HashSet<string> ScannedFiles = new HashSet<string>();

        public static void Start_Rigistery_Timer()
        {
            Timer timer = new System.Timers.Timer();
            timer.Interval = 20000; // 20 seconds
            timer.Elapsed += ListProgramsInRunKey;
            timer.Start();
            while (true)
            {
                // Keep the thread alive
            }
        }
        private static void ListProgramsInRunKey(object sender, System.Timers.ElapsedEventArgs e)
        {
           
            const string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    
                    foreach (string valueName in key.GetValueNames())
                    {
                        if (!ScannedFiles.Contains(valueName))
                        {
                            ScannedFiles.Add(valueName);
                            FileToScan fts = new FileToScan(key.GetValue(valueName)?.ToString(), $"suspicious file found at registery found at {key.GetValue(valueName)?.ToString()} - file rename");
                            AVEngine.QueueFileForScan(fts);
                        }
                        
                    }
                }
            }
        }
    }
}
