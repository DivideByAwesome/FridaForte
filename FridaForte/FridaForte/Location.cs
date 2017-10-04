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
        public string Hint { get; }
        public string[] Choices;
        public string Danger { get; }
        public string CorrectChoice { get; }

        public Location(string name, string message, string hint, string[] choices, string correctChoice, string danger)
        {
            Name = name;
            Message = message;
            Hint = hint;
            Choices = choices;
            Danger = danger;
            CorrectChoice = correctChoice;
        }

        public void ShowChoices()
        {
            WriteLine("\nWhat do you want to do?\n");

            WriteLine(Choices[0]);
            for (int i = 1; i < Choices.Length; i++)
            {
                WriteLine("or");
                WriteLine(Choices[i]);
            }
        }
    }
}
