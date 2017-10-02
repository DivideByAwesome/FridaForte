using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridaForte
{
    public class Player
    {
        public string Name { get; }
        public string CurrentInput { get; }

        public Player(string name, string currentInput)
        {
            Name = name;
            CurrentInput = currentInput;
        }
    }
}
