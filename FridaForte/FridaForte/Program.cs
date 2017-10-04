﻿using System;
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
            string input = string.Empty;
            Player player = new Player();
            
            Location[] locations = GetContent();
            WelcomePlayer(player);                    
            Typer(locations[0].Name);
            Typer(WordWrapper(locations[0].Message));
            locations[0].ShowChoices();
            input = GetInput("\nEnter your decision: ");
            
            ReadKey(); // This command pauses the console so user has time to read it and dev has time to see results.
        } // End Main()

        public static Location[] GetContent()
        {
            string path = Directory.GetCurrentDirectory();

            string jsonFile = path + "../../../GameContent.json";
            Location[] locations = JsonConvert.DeserializeObject<Location[]>(File.ReadAllText(jsonFile));

            // need to change this loop to a switch or if/else statement
            for (int i = 0; i < locations.Length; i++)
            {
                WriteLine(locations[i].Name);
                WriteLine(locations[i].Message);
                WriteLine("\n\n***********");
                WriteLine("Choices");
                WriteLine("***********");
                WriteLine(locations[i].Choices[0]);
                WriteLine(locations[i].Choices[1]);
            }

            return locations;
        }

        private static void WelcomePlayer(Player player)
        {
            Typer($"{player.FirstName} {player.LastName} Pharmacist Extraordinaire");
            Typer("Welcome Player!");
            Typer($"You are taking the role of {player.FirstName} {player.LastName} Pharmacist Extraordinaire! {player.FirstName} has had a modest and quiet life so far, but all of that is about to change.");
        }

        public static string GetInput(string prompt)
        {
            return Validator.ValidateString(prompt);
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
                System.Threading.Thread.Sleep(6);
            }
            WriteLine();
        }
    }
}
