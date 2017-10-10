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
        //Start Game
        static void RunGame()
        {
            Location[] locations = GetContent("BasePath");
            LocationIterator(locations);

            locations = GetContent("Connector");

            locations = GetPath(locations);
            Clear();
            LocationIterator(locations);

            locations = GetContent("Ending");
            LocationIterator(locations);
            Clear();

            ResetGame();
        }

        public static Location[] GetPath(Location[] locations)
        {
            BackgroundColor = ConsoleColor.DarkMagenta;
            Typer("Location: " + locations[0].Name);
            ResetColor();
            Typer(WordWrapper(locations[0].Message));
            locations[0].ShowChoices();
            string input;
            char[] seperatorChars = { ' ', ',' };
            string[] words;
            Location[] path = { };
            GameWorld gameWorld = InitGameWorld(GetContent("GameContent"));

            bool isWrongChoice = false;
            bool isCorrectChoice = false;

            bool hasOtherLocationCorrectWord = false;
            bool hasOtherLocationDangerWord = false;

            bool isPath1;
            bool isPath2;


            do
            {
                input = GetInput("\nEnter your decision: ");
                words = input.Split(seperatorChars);

                hasOtherLocationCorrectWord = IsFoundUniqueWords(gameWorld.AllCorrectUniqueWords, words);
                hasOtherLocationDangerWord = IsFoundUniqueWords(gameWorld.AllDangerUniqueWords, words);

                isCorrectChoice = IsFoundUniqueWords(locations[0].CorrectUniqueWords, words);
                isWrongChoice = IsFoundUniqueWords(locations[0].DangerUniqueWords, words);

                isPath1 = IsFoundUniqueWords(locations[0].CorrectUniqueWords, words);
                isPath2 = IsFoundUniqueWords(locations[0].DangerUniqueWords, words);

                if (isPath1 && !isPath2)
                {
                    path = GetContent("PathOne");
                }
                else if (isPath2 && !isPath1)
                {
                    path = GetContent("PathTwo");
                }
                else if (isCorrectChoice && isWrongChoice)
                {
                    DisplayError(input, "Please be more specific");
                }
                else if (!isCorrectChoice && !isWrongChoice && (hasOtherLocationCorrectWord || hasOtherLocationDangerWord))
                {
                    DisplayError(input, "That command doesn't apply here");
                }
                else // user inputs neither danger choice nor correct choice,
                {
                    DisplayError(input, "I don't understand that command.");
                }

            } while (!(isPath1 || isPath2) || (isPath1 && isPath2));

            return path;
            //canContinue = CanContinue(location, canContinue, gameWorld);
        }

        public static void LocationIterator(Location[] locations)
        {
            Location location;
            bool canContinue = true;
            GameWorld gameWorld = InitGameWorld(GetContent("GameContent"));

            for (int i = 0; i < locations.Length && canContinue; ++i)
            {
                location = locations[i];
                BackgroundColor = ConsoleColor.DarkMagenta;
                Typer("Location: " + location.Name);
                ResetColor();
                Typer(WordWrapper(location.Message));
                canContinue = CanContinue(location, canContinue, gameWorld);
            }
        }

        public static GameWorld InitGameWorld(Location[] locations)
        {
            GameWorld gameWorld = new GameWorld();
            foreach (Location loc in locations)
            {
                gameWorld.AllCorrectUniqueWords.UnionWith(loc.CorrectUniqueWords);
                gameWorld.AllDangerUniqueWords.UnionWith(loc.DangerUniqueWords);
            }
            return gameWorld;
        }

        public static bool CanContinue(Location location, bool canContinue, GameWorld gameWorld)
        {
            string input = string.Empty;
            bool isWrongChoice = false;
            bool isCorrectChoice = false;
            bool hasOtherLocationCorrectWord = false;
            bool hasOtherLocationDangerWord = false;

            do
            {
                location.ShowChoices();
                input = GetInput("\nEnter your decision: ");
                // add method to make input into an array to check in each Choice array for matches
                // splits input into array of words
                char[] seperatorChars = { ' ', ',' };
                string[] words = input.Split(seperatorChars);

                isCorrectChoice = IsFoundUniqueWords(location.CorrectUniqueWords, words);
                isWrongChoice = IsFoundUniqueWords(location.DangerUniqueWords, words);
                hasOtherLocationCorrectWord = IsFoundUniqueWords(gameWorld.AllCorrectUniqueWords, words);
                hasOtherLocationDangerWord = IsFoundUniqueWords(gameWorld.AllDangerUniqueWords, words);

                if (isWrongChoice && !isCorrectChoice)
                {
                    Typer(WordWrapper($"\n{location.Danger}"));
                    canContinue = false;
                    ResetGame();
                }
                else if (isCorrectChoice && !isWrongChoice)
                {
                    Typer(WordWrapper($"\n{location.CorrectChoice}"));
                    Typer("\nPress any key to continue...");
                    canContinue = true;
                }
                else if (isCorrectChoice && isWrongChoice)
                {
                    DisplayError(input, "Please be more specific");
                }
                else if (!isCorrectChoice && !isWrongChoice && (hasOtherLocationCorrectWord || hasOtherLocationDangerWord))
                {
                    DisplayError(input, "That command doesn't apply here");
                }
                else // user inputs neither danger choice nor correct choice,
                {
                    DisplayError(input, "I don't understand that command.");
                }

            } while (!(isWrongChoice || isCorrectChoice) || (isWrongChoice && isCorrectChoice));

            ReadKey();
            Clear();

            return canContinue;
        }

        public static void DisplayError(string input, string error)
        {
            WriteLine("************************");
            ForegroundColor = ConsoleColor.Red;
            Typer($"You entered: {input}");
            ResetColor();
            Typer(error);
            WriteLine("************************");
            Typer("Please try again.");
        }

        public static bool IsFoundUniqueWords(IEnumerable<string> uniqueWords, string[] words)
        {
            for (int i = 0; i < words.Length; ++i)
            {
                if (uniqueWords.Contains(words[i].ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static Location[] GetContent(string gameContent)
        {
            string path = Directory.GetCurrentDirectory();

            string jsonFile = $"{path}../../../GameContent/{gameContent}.json";
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
            Typer("\nPress any key to continue...");
            ReadKey();
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

        public static void ResetGame()
        {
            Typer("\nWould you like to play again, Yes or No: ");
            string input = ReadLine().ToLower();
            if (input == "yes" || input == "y")
            {
                Clear();
                WelcomePlayer();
                RunGame();
            }
            else if (input == "no" || input == "n")
            {
                Clear();
                ShowAuthors();
                Typer("\n\nPress \"CRTL\" and \"C\" to close the window\n");
            }
        }

        //Typewriter effect
        internal static void Typer(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Write(str[i]);
                Thread.Sleep(1);
            }
            WriteLine();
        }
    }
}
