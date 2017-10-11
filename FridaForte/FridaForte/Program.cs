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

            int wrongChoice = 0;
            int correctChoice = 0;

            int otherLocationCorrectWord = 0;
            int otherLocationDangerWord = 0;

            int path1;
            int path2;


            do
            {
                input = GetInput("\nEnter your decision: ");
                words = input.Split(seperatorChars);

                otherLocationCorrectWord = FoundUniqueWords(gameWorld.AllCorrectUniqueWords, words);
                otherLocationDangerWord = FoundUniqueWords(gameWorld.AllDangerUniqueWords, words);

                correctChoice = FoundUniqueWords(locations[0].CorrectUniqueWords, words);
                wrongChoice = FoundUniqueWords(locations[0].DangerUniqueWords, words);

                path1 = FoundUniqueWords(locations[0].CorrectUniqueWords, words);
                path2 = FoundUniqueWords(locations[0].DangerUniqueWords, words);

                if (path1 > path2)
                {
                    path = GetContent("PathOne");
                }
                else if (path2 > path1)
                {
                    path = GetContent("PathTwo");
                }
                else if (correctChoice == wrongChoice && correctChoice > 0)
                {
                    DisplayError(input, "Please be more specific");
                }
                else if (otherLocationCorrectWord > 0 || otherLocationDangerWord > 0)
                {
                    DisplayError(input, "That command doesn't apply here");
                }
                else // user inputs neither danger choice nor correct choice,
                {
                    DisplayError(input, "I don't understand that command.");
                }

            } while ((path1 < 1 && path2 < 1) || (correctChoice == wrongChoice && correctChoice > 0));

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
            int wrongChoice = 0;
            int correctChoice = 0;
            int otherLocationCorrectWord = 0;
            int otherLocationDangerWord = 0;

            do
            {
                location.ShowChoices();
                input = GetInput("\nEnter your decision: ");
                // add method to make input into an array to check in each Choice array for matches
                // splits input into array of words
                char[] seperatorChars = { ' ', ',' };
                string[] words = input.Split(seperatorChars);

                correctChoice = FoundUniqueWords(location.CorrectUniqueWords, words);
                wrongChoice = FoundUniqueWords(location.DangerUniqueWords, words);
                otherLocationCorrectWord = FoundUniqueWords(gameWorld.AllCorrectUniqueWords, words);
                otherLocationDangerWord = FoundUniqueWords(gameWorld.AllDangerUniqueWords, words);

                if (wrongChoice > correctChoice)
                {
                    Typer(WordWrapper($"\n{location.Danger}"));
                    canContinue = false;
                    ResetGame();
                }
                else if (correctChoice > wrongChoice)
                {
                    Typer(WordWrapper($"\n{location.CorrectChoice}"));
                    Typer("\nPress any key to continue...");
                    canContinue = true;
                }
                else if (correctChoice == wrongChoice && correctChoice > 0)
                {
                    DisplayError(input, "Please be more specific");
                }
                else if (otherLocationCorrectWord > 0 || otherLocationDangerWord > 0)
                {
                    DisplayError(input, "That command doesn't apply here");
                }
                else // user inputs neither danger choice nor correct choice nor other location word 
                {
                    DisplayError(input, "I don't understand that command.");
                }

            } while ((wrongChoice < 1 && correctChoice < 1) || (correctChoice == wrongChoice && correctChoice > 0));
           
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

        public static int FoundUniqueWords(IEnumerable<string> uniqueWords, string[] words)
        {
            int counter = 0;
            for (int i = 0; i < words.Length; ++i)
            {
                if (uniqueWords.Contains(words[i].ToLower()))
                {
                    ++counter;
                }//Else keep looping
            }
            return counter;
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
