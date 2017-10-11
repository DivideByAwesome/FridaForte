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

                try
                {
                    input = ReadLine().Trim();
                }
                catch (NullReferenceException)
                {
                    input = string.Empty;
                }
                catch (Exception)
                {
                    input = string.Empty;
                }
            } while (input.Length < 1 || input.Length > 50);

            return input;
        }
    }
}
