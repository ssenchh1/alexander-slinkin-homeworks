using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Channels;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            
            game.Start();

            Console.WriteLine();
        }
        
    }
}
