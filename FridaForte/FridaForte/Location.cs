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
        public string[] wrongChoices;
        public string RightChoice { get; }

        public Location(string name, string message, string hint, string[] wrongChoices, string rightChoice)
        {
            Name = name;
            Message = message;
            Hint = hint;
            this.wrongChoices = wrongChoices;
            RightChoice = rightChoice;
        }

        public void ShowWrongChoices()
        {
            WriteLine("\nWhat do you want to do?\n");

            WriteLine(wrongChoices[0]);
            for (int i = 1; i < wrongChoices.Length; i++)
            {
                WriteLine("or");
                WriteLine(wrongChoices[i]);
            }
        }
    }
}
