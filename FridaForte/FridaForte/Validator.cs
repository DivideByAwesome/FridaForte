using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FridaForte
{
    public static class Validator
    {
        public static string ValidateString(string prompt)
        {
            string input = string.Empty;

            do
            {
                Write(prompt);
                input = ReadLine().Trim();
            } while (input.Length < 2 || input.Length > 50);

            return input;
        }
    }
}
