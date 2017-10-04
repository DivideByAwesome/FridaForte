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
        public string[] choices;

        public Location(string name, string message, string hint, string[] choices)
        {
            Name = name;
            Message = message;
            Hint = hint;
            this.choices = choices;
        }

        public void ShowChoices()
        {
            WriteLine("\nWhat do you want to do?\n");

            WriteLine(choices[0]);
            for (int i = 1; i < choices.Length; i++)
            {
                WriteLine("or");
                WriteLine(choices[i]);
            }
        }
    }
}
