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

        public void PlayCard(int cardNumber, Deck tableDeck)
        {
            Card pullCard = mDeck.PullIndexCard(cardNumber);
            tableDeck.PushCardForColor(pullCard);
        }

        public void DropCard(int cardNumber)
        {
            Card pullCard = mDeck.PullIndexCard(cardNumber);
            mDropDeck.PushCardForColor(pullCard);
        }

        public void TellColor(Command command, Deck nextPlayerDeck)
        {
            nextPlayerDeck.ChangeStatusColorVisible(command.CardColor, command.CardIndexes);
        }

        public void TellRank(Command command, Deck nextPlayerDeck)
        {
            nextPlayerDeck.ChangeStatusRankVisible(command.CardRank, command.CardIndexes);
        }
    }
}
