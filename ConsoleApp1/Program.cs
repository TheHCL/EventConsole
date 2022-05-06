using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Diagnostics;


namespace ConsoleApp1
{
    internal class Program
    {

        public static void Get_cmd(string fname, string arguments)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = fname,
                Arguments = arguments,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };
            process.Start();

            
            process.WaitForExit();
            Console.WriteLine("System Event log Clear.");
            
            
        }
        static void Main(string[] args)
        {
            int nums=0;
            string pa="";
            if (args.Length == 1)
            {
                string erase = args[0];
                if (erase == "clean" || erase == "clear")
                {
                    Get_cmd("wevtutil", "cl system");
                }
            }
            if (args.Length == 2)
            {
                nums = Convert.ToInt16(args[0]);
                pa = args[1];
                var reader = new EventLogReader("C:\\Windows\\System32\\winevt\\Logs\\System.evtx", PathType.FilePath);
                EventRecord record;
                string text = "";
                int count = 0;
                while ((record = reader.ReadEvent()) != null)
                {
                    if (nums == -1 && pa != "-1")
                    {
                        try
                        {
                            if (record.FormatDescription().ToString().ToLower().Contains(pa))
                            {
                                count++;
                                text += record.TimeCreated.ToString() + "  " + record.Id.ToString() + "  " + record.FormatDescription().ToString() + "\n";

                            }
                        }
                        catch { }

                    }
                    if (nums != -1 && pa == "-1")
                    {
                        try
                        {
                            if (record.Id == nums)
                            {
                                count++;
                                text += record.TimeCreated.ToString() + "  " + record.Id.ToString() + "  " + record.FormatDescription().ToString() + "\n";
                            }
                        }
                        catch { }

                    }
                    if (nums != -1 && pa != "-1")
                    {
                        try
                        {
                            if (record.FormatDescription().ToString().ToLower().Contains(pa))
                            {
                                count++;
                                text += record.TimeCreated.ToString() + "  " + record.Id.ToString() + "  " + record.FormatDescription().ToString() + "\n";
                            }
                        }
                        catch { }

                    }






                }

                if (text == "")
                {
                    text = "No events were found";
                }
                File.WriteAllText("eventlog.txt", text);
            }
            

            



        }
    }
}
