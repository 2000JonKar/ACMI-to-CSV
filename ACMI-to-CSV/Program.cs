using System;
using System.IO;

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
                string[] acmi = File.ReadAllLines(file.FullName); // Läs in och spara alla rader i respektive .acmi fil
                File.WriteAllLines($"G:\\My Drive\\DCS\\TacView\\Data\\CSV\\CSV_{file}.csv", acmi); // Skapa .csv fil för att importera till Google Sheets
                Console.WriteLine(file.Name); // Skriv ut .acmi-filnamn för att bekräfta
            }
            Console.ReadLine(); // Låt användare titta hur länge dom vill på resultaten
        }
    }
}
