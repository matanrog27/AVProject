using AV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus
{
    public class DirectoryWatcher

    {
        public static  string logPath = @"..\..\LogFile.log";


        public static void InitWatcher (string directoryToWatch)
        {
            try
            {
                // Create a new file system watcher instance
                FileSystemWatcher watcher = new FileSystemWatcher();

                // Set the directory to monitor
                watcher.Path = directoryToWatch;

                // Only watch for changes to files, not directories
                watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

                // Attach event handlers for the different types of file system changes
                watcher.Created += OnFileCreated;
                watcher.Changed += OnFileChanged;
                watcher.Deleted += OnFileDeleted;
                watcher.Renamed += OnFileRenamed;

                // Start the file system monitor
                watcher.EnableRaisingEvents = true;

                // Keep the console application running
                Console.WriteLine($"Monitoring directory: {directoryToWatch}");
                Console.ReadLine();
            }
            catch(Exception ex ) 
            {
                using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                {
                    // Write an initial message or header
                    writer.WriteLine(DateTime.Now + $" failed listen to directory: {ex.Message}");
                }
            }


          
        }
        static void OnFileCreated(object source, FileSystemEventArgs e)
        {
            
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                // Write an initial message or header
                writer.WriteLine(DateTime.Now+ $" WARNING! File created: {e.FullPath}");
            }
            AVEngine.QueueFileForScan(e.FullPath);
        }

        static void OnFileChanged(object source, FileSystemEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                // Write an initial message or header
                writer.WriteLine(DateTime.Now + $" WARNING! File changed: {e.FullPath}");
            }
            AVEngine.QueueFileForScan(e.FullPath);
        }

        static void OnFileDeleted(object source, FileSystemEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                // Write an initial message or header
                writer.WriteLine(DateTime.Now + $" WARNING! File deleted: {e.FullPath}");
            }
            AVEngine.QueueFileForScan(e.FullPath);
        }

        static void OnFileRenamed(object source, RenamedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                // Write an initial message or header
                writer.WriteLine(DateTime.Now + $" WARNING! File renamed: {e.FullPath}");
            }
            AVEngine.QueueFileForScan(e.FullPath);
        }

    }
}
