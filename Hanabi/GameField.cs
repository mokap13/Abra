using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class GameField
    {
        public List<Card> mainDeck;
        public List<Card> tableDeck;
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
            
            tableDeck = new List<Card>();
            mainDeck = new List<Card>();
        }
    }
}
