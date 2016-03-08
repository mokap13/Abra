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

        public void TakeCardFromDeck(Deck sourceDeck)
        {
            mDeck.Cards.Add(sourceDeck.Cards.First());
            sourceDeck.Cards.RemoveAt(0);
        }

        public void PlayCard(GameField gameField, Command command)
        {
            Card pullCard = mDeck.PullIndexCard(command.ChoosedCards[0]);
            gameField.tableDeck.PushCardForColor(pullCard);
        }

        public void DropCard(Command command)
        {
            mDeck.Cards.RemoveAt(command.ChoosedCards[0]);
        }

        public void TellColor(GameField gameField, Command command)
        {
            Console.WriteLine();
        }

        public void TellRank(GameField gameField, Command command)
        {
            Console.WriteLine();
        }
    }
}
