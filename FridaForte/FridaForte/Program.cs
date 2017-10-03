using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; // Now lazy devs don't have to type "Console" while coding!
// Be sure to check Newtonsoft.json is downloaded.
using Newtonsoft.Json;
using System.IO;

namespace FridaForte
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Location pharmacy = new Location("Pharmacy");
            //Location home = new Location("Home");
            //Location fortPoint = new Location("Fort Point");
            //Location meadow = new Location("Meadow");
            //Location jail = new Location("Jail");
            //Location cousinHouse = new Location("Cousin's House");
            string jsonFile = @"C:\Users\Student\Source\Repos\FridaForte\FridaForte\FridaForte\GameContent.json";
            Location[] locations = JsonConvert.DeserializeObject<Location[]>(File.ReadAllText(jsonFile));
            for(int i = 0; i < locations.Length; i++)
            {
                WriteLine(locations[i].Name);
                WriteLine(locations[i].Message);
                WriteLine("\n\n***********");
                WriteLine("Choices");
                WriteLine("***********");
                WriteLine(locations[i].Choices[0]);
                WriteLine(locations[i].Choices[1]);
            }



            ReadKey(); // This command pauses the console so user has time to read it and dev has time to see results.
        }
    }
}
