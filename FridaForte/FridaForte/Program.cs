using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ShowAuthors();
            ReadKey(); // This command pauses the console so user has time to read it and dev has time to see results.
        } // End Main()

        private static void ShowAuthors()
        {
            WriteLine("\nBrought to you by the A-Team:\n");
            WriteLine("Scrum Master/Fearless Leader: Sara Jade https://www.linkedin.com/in/sara-jade/");
            WriteLine("Super Coding Diva: Sugey Valencia https://www.linkedin.com/in/sugey-valencia-955667140/");
            WriteLine("Rockin' Feature Developer: Roscoe Bass III https://www.linkedin.com/in/roscoebass/");
            WriteLine("Awesome UX Dev: Alem Asefa https://www.linkedin.com/in/alemneh/");
        }

        private static void RunGame()
        {
            Location[] locations = GetContent();
            Location location;
            bool canContinue = true;
            
            for (int i = 0; i < locations.Length && canContinue; ++i)
            {
                location = locations[i];
                BackgroundColor = ConsoleColor.DarkMagenta;
                Typer("Location: " + location.Name);
                ResetColor();
                Typer(WordWrapper(location.Message));
                canContinue = CanContinue(location, canContinue);
            }
            Typer("\n\nPress \"CTRL\" and \"C\" to close the window\n");
        }

        private static bool CanContinue(Location location, bool canContinue)
        {
            string input = string.Empty;
            bool isWrongChoice = false;
            bool isCorrectChoice = false;
            
            do
            {
                location.ShowChoices();
                input = GetInput("\nEnter your decision: ");
                isWrongChoice = location.DangerUniqueWords.Contains(input.ToLower());
                isCorrectChoice = location.CorrectUniqueWords.Contains(input.ToLower());

                if (isWrongChoice && !isCorrectChoice)
                {
                    Typer(WordWrapper($"\n{location.Danger}"));
                    canContinue = false;
                }
                else if (isCorrectChoice && !isWrongChoice)
                {
                    Typer(WordWrapper($"\n{location.CorrectChoice}"));
                    canContinue = true;
                }
                else // user inputs neither danger choice nor correct choice,
                     // or user inputs both correct and danger
                {
                    WriteLine("************************");
                    ForegroundColor = ConsoleColor.Red;
                    Typer($"You entered: {input}");
                    ResetColor();
                    Typer("I don't understand that command.");
                    WriteLine("************************");
                    Typer("Please try again.");
                }
            } while (!(isWrongChoice || isCorrectChoice) || (isWrongChoice && isCorrectChoice));

            ReadKey();
            Clear();

            return canContinue;
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
            Typer("************************************************\n");
            Typer($"{player.FirstName} {player.LastName} Pharmacist Extraordinaire\n");
            Typer("************************************************");
            Typer("\nWelcome Player!");
            Typer($"\nYou are taking the role of {player.FirstName} {player.LastName} Pharmacist Extraordinaire!\n{player.FirstName} has had a modest and quiet life so far, but all of that is\nabout to change.");
            ReadLine();
            Clear();
        }

        public static string GetInput(string prompt)
        {
            return Validator.ValidateString(prompt).ToLower();
        }

        //Word wrapper
        internal static string WordWrapper(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return string.Empty;
            }

            int approxLineCount = paragraph.Length / 80;
            StringBuilder lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

            for (int i = 0; i < paragraph.Length;)
            {
                int grabLimit = Math.Min(80, paragraph.Length - i);
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

        //Typewriter effect
        internal static void Typer(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Write(str[i]);
                System.Threading.Thread.Sleep(1);
            }
            WriteLine();
        }
    }
}
