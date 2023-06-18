using AV;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AntiVirus
{
    public static class PortListener
    {
        private static HashSet<Port> Ports = new HashSet<Port>();
        public static void InitPortListener()
        {
           

            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.Arguments = "-a -n -o";
                    ps.FileName = "netstat.exe";
                    ps.UseShellExecute = false;
                    ps.WindowStyle = ProcessWindowStyle.Hidden;
                    ps.RedirectStandardInput = true;
                    ps.RedirectStandardOutput = true;
                    ps.RedirectStandardError = true;

                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    if (exitStatus != "0")
                    {
                        // Command Errored. Handle Here If Need Be
                    }

                    //Get The Rows
                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        //Split it baby
                        string[] tokens = Regex.Split(row, "\\s+");
                        if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
                        {
                            string localAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");


                            Port port = new Port
                            {
                                protocol = localAddress.Contains("1.1.1.1") ? String.Format("{0}v6", tokens[1]) : String.Format("{0}v4", tokens[1]),
                                port_number = localAddress.Split(':')[1],
                                exe_file = tokens[1] == "UDP" ? LookupExecutable(Convert.ToInt16(tokens[4])) : LookupExecutable(Convert.ToInt16(tokens[5]))
                            };

                            if (!Ports.Contains(port))
                            {
                                if (port.exe_file != "-")
                                {
                                    Ports.Add(port);
                                    FileToScan fts = new FileToScan(port.exe_file, "suspicious file opened port");
                                    AVEngine.QueueFileForScan(fts);
                                }
                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static string LookupExecutable(int pid)
        {
            string exePath;
            try { exePath = Process.GetProcessById(pid).MainModule.FileName; }
            catch (Exception) { exePath = "-"; }
            return exePath;
        }


    }
}