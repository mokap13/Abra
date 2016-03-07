using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Player
    {
        private Deck mDeck;

        public Player()
        {
            mDeck = new Deck();
        }

        public Deck Deck
        {
            get
            {
                return mDeck;
            }
        }

        private void PlayCard(GameField gameField, int choosedCard)
        {
            
        }

        private void DropCard(GameField gameField, int choosedCard)
        {
            
        }

        private void TellColor(GameField gameField, CardColor cardColor, int[] choosedCards)
        {
            Console.WriteLine();
        }

        private void TellRank(GameField gameField, CardRank cardValue, int[] choosedCards)
        {
            Console.WriteLine();
        }
    }
}
