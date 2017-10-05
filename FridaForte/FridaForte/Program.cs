using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; // Now lazy devs don't have to type "Console" while coding!

namespace FridaForte
{
    public class Program
    {
        static void Main(string[] args)
        {                      
            WelcomePlayer();
            RunGame();        
            ReadKey(); // This command pauses the console so user has time to read it and dev has time to see results.
        } // End Main()

        private static void RunGame()
        {
            Location[] locations = GetContent();
            string input = string.Empty;
            bool isWrongChoice = false;
            bool isCorrectChoice = false;

            for (int i = 0; i < locations.Length; ++i)
            {
                Typer(locations[i].Name);
                Typer(WordWrapper(locations[i].Message));
                
                do
                {
                    locations[i].ShowChoices();
                    input = GetInput("\nEnter your decision: ");
                    isWrongChoice = input.Contains(locations[i].Choices[0].ToLower());
                    isCorrectChoice = input.Contains(locations[i].Choices[1].ToLower());

                    if (isWrongChoice)
                    {
                        Typer(locations[i].Danger);
                        return;
                    }
                    else if (isCorrectChoice)
                    {
                        Typer(locations[i].CorrectChoice);
                    }
                    else // user inputs neither danger choice nor correct choice
                    {
                        WriteLine($"\nYou entered: {input}");
                        WriteLine("I don't understand that command.\nPlease try again.");
                    }
                } while (!(isWrongChoice || isCorrectChoice));
            }
        }

        public static Location[] GetContent()
        {
            string path = Directory.GetCurrentDirectory();

            string jsonFile = path + "../../../GameContent.json";
            Location[] locations = JsonConvert.DeserializeObject<Location[]>(File.ReadAllText(jsonFile));

            return locations;
        }

        private static void WelcomePlayer()
        {
            Player player = new Player();

            Typer($"{player.FirstName} {player.LastName} Pharmacist Extraordinaire");
            Typer("\nWelcome Player!");
            Typer($"\nYou are taking the role of {player.FirstName} {player.LastName} Pharmacist Extraordinaire! {player.FirstName} has had a modest and quiet life so far, but all of that is about to change.");
        }

        public static string GetInput(string prompt)
        {
            return Validator.ValidateString(prompt).ToLower();
        }

        static string WordWrapper(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return string.Empty;
            }

            int approxLineCount = paragraph.Length / WindowWidth;
            StringBuilder lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

            for (int i = 0; i < paragraph.Length;)
            {
                int grabLimit = Math.Min(WindowWidth, paragraph.Length - i);
                string line = paragraph.Substring(i, grabLimit);

                bool isLastChunk = grabLimit + i == paragraph.Length;

                if (isLastChunk)
                {
                    i = i + grabLimit;
                    lines.Append(line);
                }
                else
                {
                    int lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
                    lines.AppendLine(line.Substring(0, lastSpace));

                    //Trailing spaces needn't be displayed as the first character on the new line
                    i = i + lastSpace + 1;
                }
            }
            return lines.ToString();
        }

        static void Typer(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Write(str[i]);
                System.Threading.Thread.Sleep(3);
            }
            WriteLine();
        }
    }
}
