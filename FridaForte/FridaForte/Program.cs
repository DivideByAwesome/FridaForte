﻿using System;
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
            Player player = new Player();
            WriteLine($"{player.FirstName} {player.LastName} Pharmacist Extraordinaire");
            WriteLine("Welcome Player!");
            WriteLine($"You are taking the role of {player.FirstName} {player.LastName} Pharmacist Extraordinaire! {player.FirstName} has had a modest and quiet life so far, but all of that is about to change.");
            //Location home = new Location("Home");
            //Location fortPoint = new Location("Fort Point");
            //Location meadow = new Location("Meadow");
            //Location jail = new Location("Jail");
            //Location cousinHouse = new Location("Cousin's House");
            string[] choices = { "stay and run shop", "go help your cousin" };
            Location pharmacy = new Location("Pharmacy", $"Today {player.FirstName} gets a letter from her cousin who lives in Fort Point, California.\n\n\nLetter:\n\nDearest Cousin {player.FirstName},\n\n\nIt is with great sadness that I inform you that I have fallen ill with dysentery. Our little town of Fort Point does not have a doctor and I am running out of time. I would normally not ask this of you but I am in great distress. As you are the only pharmacist I know, I see no better person to help me in my time of need. Would you please come as soon as possible?\n\n\nYour loving cousin,\n\nAsher", "no hint for this scene", choices);

            Typer(pharmacy.Name);
            Typer(WordWrapper(pharmacy.Message));



            ReadKey(); // This command pauses the console so user has time to read it and dev has time to see results.
        }

        static string WordWrapper(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return string.Empty;
            }

            var approxLineCount = paragraph.Length / Console.WindowWidth;
            var lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

            for (var i = 0; i < paragraph.Length;)
            {
                var grabLimit = Math.Min(Console.WindowWidth, paragraph.Length - i);
                var line = paragraph.Substring(i, grabLimit);

                var isLastChunk = grabLimit + i == paragraph.Length;

                if (isLastChunk)
                {
                    i = i + grabLimit;
                    lines.Append(line);
                }
                else
                {
                    var lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
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
                System.Threading.Thread.Sleep(8);
            }
            WriteLine();
        }
    }
}
