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
        public Deck garbageDeck;

        public Player currentPlayer;
        public Player nextPlayer;

        public int turn;
        public int score;
        public int risk;
        public bool finished;

        public GameField()
        {
            turn = 0;
            score = 0;
            risk = 0;
            finished = false;

            mainDeck = new Deck();
            tableDeck = new Deck();
            garbageDeck = new Deck();
            tableDeck.Cards.Add(new Card('R', '0'));
            tableDeck.Cards.Add(new Card('G', '0'));
            tableDeck.Cards.Add(new Card('B', '0'));
            tableDeck.Cards.Add(new Card('Y', '0'));
            tableDeck.Cards.Add(new Card('W', '0'));
            tableDeck.Name = "Table: ";
        }

        /// <summary>
        /// Обновляет статусы игроков
        /// </summary>
        public void UpdatePlayerStatus()
        {
            currentPlayer.Deck.Name = "Current player: ";
            nextPlayer.Deck.Name = "Next player: ";
        }
    }
}
