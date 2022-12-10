using System;
using System.Collections.Generic;
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

            // Efter långa förhandlingar, lite vilja, en aning våld, och jävligt med vaselin fick jag äntligen ordning på denna foreach
            foreach (FileInfo file in Files)
            {
                string path = file.FullName;
                int lines = File.ReadLines(path).Count();
                string[] data = File.ReadAllLines(path);

                // Välj varje linje från .acmi som "pratar" om robotar och kommer från BLUFOR.
                var query = from line in data where line.Contains("Type=Weapon+Missile") && line.Contains("Color=Blue") select line;

                // Skapa en ny lista som ska innehålla alla query resultat
                List<string> acmi = new List<string>();

                // Foreach för att lägga till varje rad till listan acmi
                foreach(string line in query)
                {
                    acmi.Add(line);
                    Console.WriteLine("{0}", line); // För att bekräfta att man faktiskt fått data så skriver vi ut varje rad
                }
                
                File.WriteAllLines($"G:\\My Drive\\DCS\\TacView\\Data\\CSV\\CSV_{file}.csv", acmi); // Skapa .csv fil för att importera till Google Sheets
                Console.WriteLine(file.Name); // Skriv ut .acmi-filnamn för att bekräfta
            }
            Console.ReadLine(); // Låt användare titta hur länge dom vill på resultaten
        }
    }
}
