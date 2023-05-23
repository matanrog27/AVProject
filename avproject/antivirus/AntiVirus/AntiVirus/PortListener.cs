using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    public class PortListener
    {
        public static void InitPortListener()
        {
            Console.WriteLine("Listening for opened ports...");

            // Retrieve all active TCP connections
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            // Get the current list of open ports
            var openPorts = connections.Select(c => c.LocalEndPoint.Port).Distinct().ToList();
            Console.WriteLine("Currently open ports: " + string.Join(", ", openPorts));

            // Start listening for new ports
            var timer = new System.Timers.Timer(5000); // Check every 5 seconds
            timer.Elapsed += (sender, e) =>
            {
                var newConnections = properties.GetActiveTcpConnections();
                var newPorts = newConnections.Select(c => c.LocalEndPoint.Port).Distinct().ToList();

                var newlyOpenedPorts = newPorts.Except(openPorts);
                if (newlyOpenedPorts.Any())
                {
                    using (StreamWriter writer = new StreamWriter(DirectoryWatcher.logPath, true))
                    {
                        // Write an initial message or header
                        writer.WriteLine(DateTime.Now + "Newly opened ports: " + string.Join(", ", newlyOpenedPorts));
                    }
                    openPorts.AddRange(newlyOpenedPorts);
                }
            };
            timer.Start();

            // Keep the program running
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            //timer.Stop();
        }


    }
}
