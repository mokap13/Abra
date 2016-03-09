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
        private Deck mDropDeck;

        public Player()
        {
            mDeck = new Deck();
            mDropDeck = new Deck();
        }

        public Deck Deck
        {
            get
            {
                return mDeck;
            }
        }

        public Deck DropDeck
        {
            get
            {
                return mDropDeck;
            }
            set
            {
                mDropDeck = value;
            }
        }

        public void TakeCardFromDeck(Deck sourceDeck)
        {
            mDeck.Cards.Add(sourceDeck.Cards.First());
            sourceDeck.Cards.RemoveAt(0);
        }

        public void PlayCard(GameField gameField, Command command)
        {
            int choosedCard = (int)command.ChoosedCards[0];
            Card pullCard = mDeck.PullIndexCard(choosedCard);
            gameField.tableDeck.PushCardForColor(pullCard);
            TakeCardFromDeck(gameField.mainDeck);
        }

        public void DropCard(GameField gameField, Command command)
        {
            int choosedCard = (int)command.ChoosedCards[0];
            Card pullCard = mDeck.PullIndexCard(choosedCard);
            mDropDeck.PushCardForColor(pullCard);
            TakeCardFromDeck(gameField.mainDeck);
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
