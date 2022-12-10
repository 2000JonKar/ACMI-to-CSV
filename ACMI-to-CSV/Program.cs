using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ACMI_to_CSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lagra directory till Google Drive Desktop mappen där .acmi data kommer finnas
            DirectoryInfo dir = new DirectoryInfo(@"G:\\My Drive\\DCS\\TacView\\Data");
            FileInfo[] Files = dir.GetFiles("*.acmi"); // Lagra de filer som slutar i .acmi

            // Loop through all files in dir directory and put them through TacView.exe to generate a more managable .csv
            foreach (FileInfo file in Files)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = $"-Open:\"G:\\My Drive\\DCS\\TacView\\Data\\{file}\" -ExportFlightLog:\"G:\\My Drive\\DCS\\TacView\\Data\\CSV\\{file}.csv\" -Quiet –Quit";
                start.FileName = "C:\\Program Files (x86)\\Tacview\\Tacview.exe";
                
                using (Process proc = Process.Start(start))
                {
                    proc.WaitForExit();
                }
            }
            Console.WriteLine("End of process...");
            Console.ReadLine(); // Låt användare titta hur länge dom vill på resultaten
        }
    }
}
