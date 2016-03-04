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
        public Player player_A;
        public Player player_B;
        public List<Card> tableDeck;

        public int Turn;
        public int Score;
        public bool Finished;
        
        public GameField()
        {
            Turn = 0;
            Score = 0;
            Finished = false;
        }
    }
}
