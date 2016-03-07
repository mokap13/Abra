using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Player
    {
        private List<Card> mDeck;

        public Player()
        {
            mDeck = new List<Card>();
        }

        public List<Card> Deck
        {
            get
            {
                return mDeck;
            }
        }

        public void ExecuteCommand(GameField gamefield, Command command)
        {

        }

        private void PlayCard(GameField gameField, int choosedCard)
        {
            gameField.tableDeck.Add(mDeck[choosedCard]);
            mDeck.RemoveAt(choosedCard);
        }

        private void DropCard(GameField gameField, int choosedCard)
        {
            mDeck.RemoveAt(choosedCard);
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
