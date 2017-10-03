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
        public void showChoices()
        {
            foreach (string choice in Choices)
            {
                WriteLine(choice);
            }
        }

        public Location(string name, string message, string hint, string[] choices)
        {
            Name = name;
            Message = message;
            Hint = hint;
            Choices = choices;
        }

    }
}
