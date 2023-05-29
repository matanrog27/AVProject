using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace AntiVirus
{
    public class ProccessMonitor
    {
        public static void InitProcessMonitor()
        {
            // Set up a query to retrieve instances of the Win32_Process class
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'");

            // Create a new management scope
            ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2");

            // Create a new event watcher
            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);

            // Set up the event handler
            watcher.EventArrived += (s, e) =>
            {
                // Get the process name and ID
                ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                string processName = (string)instance["Name"];
                uint processId = (uint)instance["ProcessId"];

                // Print the process name and ID
                // MessageBox.Show("Process '{0}' (PID {1}) started", processName, (MessageBoxButtons)processId);
                 MessageBox.Show("process created");
            };

            // Start the watcher
            watcher.Start();
            while (true) { }
            // Wait for user input to stop the program
            Console.WriteLine("Press any key to stop the program...");
            Console.Read();

            // Stop the watcher
            watcher.Stop();
        }
    }
}
