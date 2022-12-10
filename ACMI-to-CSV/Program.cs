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

                string path = "G:\\My Drive\\DCS\\TacView\\Data\\CSV\\" + file.Name + ".csv";
                int lines = File.ReadLines(path).Count();
                string[] data = File.ReadAllLines(path);

                var query = from line in data where line.Contains("HasFired") && line.Contains("Enemies") select line;

                List<string> newCSV = new List<string>();

                foreach(string line in query)
                {
                    newCSV.Add(line);
                }

                File.WriteAllLines($"G:\\My Drive\\DCS\\TacView\\Data\\CSV\\CSV_{file}.csv", newCSV);
                Console.WriteLine(file.FullName);
            }
            Console.WriteLine("End of process...");
            Console.ReadLine(); // Låt användare titta hur länge dom vill på resultaten
        }
    }
}
