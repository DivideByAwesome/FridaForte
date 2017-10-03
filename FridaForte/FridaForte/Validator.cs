using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridaForte
{
    public static class Validator
    {
        public static string ValidateString(string input)
        {
            if(input.Length > 50 || input.Length < 2)
            {
                return "I don't understand that command.";
            }
            return input;
        }
    }
}
