using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    static class Output
    {
        public static void ShowDeck(List<Card> deck)
        {
            foreach (var card in deck)
            {
                card.Show();
                Console.Write(" ");
            }
            Console.Write("\n");
        }
    }
}
