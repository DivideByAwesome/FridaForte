using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FridaForte
{
    public class Location
    {
        public string Name { get; }
        public string Message { get; }
        public string[] ChoiceContext;
        public string[] Choices;
        public string[] UniqueWords;
        public string Danger { get; }
        public string CorrectChoice { get; }

        public Location(string name, string message, string[] choiceContext, string[] choices, string correctChoice, string danger, string[] uniqueWords)
        {
            Name = name;
            Message = message;
            ChoiceContext = choiceContext;
            Choices = choices;
            UniqueWords = uniqueWords;
            Danger = danger;
            CorrectChoice = correctChoice;
        }

        public void ShowChoices()
        {
            Program.Typer("\nWhat do you want to do?\n");
            ForegroundColor = ConsoleColor.Gray;
            BackgroundColor = ConsoleColor.DarkGreen;
            WriteLine(Choices[0]);
            ResetColor();
            if (!(ChoiceContext[0] == null))
            {
                ForegroundColor = ConsoleColor.DarkGreen;
                Write(Program.WordWrapper(ChoiceContext[0] + "\n"));
            }

            for (int i = 1; i < Choices.Length; i++)
            {
                ResetColor();
                WriteLine("-or-");
                ForegroundColor = ConsoleColor.Gray;
                BackgroundColor = ConsoleColor.DarkGreen;
                WriteLine(Choices[i]);
                ResetColor();
                if (!(ChoiceContext[i] == null))
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write(Program.WordWrapper(ChoiceContext[i] + "\n"));
                }
                ResetColor();
            }
        }

    }
}
