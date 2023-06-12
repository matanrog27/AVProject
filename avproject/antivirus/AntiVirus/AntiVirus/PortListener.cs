using System;
using System.Management;
namespace AntiVirus
{
    class PortListener
    {
        public static void InitPortListener()
        {
            // Create a WMI query to monitor network connections
            string query = @"SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_PerfFormattedData_Tcpip_TCPv4'";

            // Create a ManagementEventWatcher to monitor the query
            ManagementEventWatcher watcher = new ManagementEventWatcher(new WqlEventQuery(query));

            // Set the event handler for new instance creation events
            watcher.EventArrived += (sender, e) =>
            {
                ManagementBaseObject newObject = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                string processId = newObject["OwningProcess"].ToString();
                string localAddress = newObject["Name"].ToString();
                Console.WriteLine($"New connection: Process ID - {processId}, Local Address - {localAddress}");
            };

            // Start monitoring
            watcher.Start();
            while (true) { }
            Console.WriteLine("Monitoring new network connections. Press Enter to exit.");
            Console.ReadLine();

            // Stop monitoring
            watcher.Stop();
        }
    }
}