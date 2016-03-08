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

        public void PlayCard(GameField gameField, Command command)
        {
            Card pullCard = gameField.currentPlayer.Deck.PullIndexCard(command.ChoosedCards[0]);
            gameField.tableDeck.PushCardForColor(pullCard);
        }

        public void DropCard(GameField gameField, Command command)
        {
            
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
