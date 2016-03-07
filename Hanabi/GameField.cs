using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class GameField
    {
        public Deck mainDeck;
        public Deck tableDeck;

        public Player currentPlayer;
        public Player nextPlayer;

        public int turn;
        public int score;
        public bool finished;
        
        public GameField()
        {
            turn = 0;
            score = 0;
            finished = false;

            mainDeck = new Deck();
            tableDeck = new Deck();
            tableDeck.Name = "Table: ";
        }

        /// <summary>
        /// Обновляет статусы игроков
        /// </summary>
        public void UpdateDecksName()
        {
            currentPlayer.Deck.Name = "Current player: ";
            nextPlayer.Deck.Name = "Next player: ";
        } 
    }
}
