using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    static class Input
    {
        private static string enterData;

        public static void EnterMainDeck(GameField gameField)
        {
            Console.WriteLine("Start new game with deck ");
            enterData = Console.ReadLine();

            

        }
    }
}
