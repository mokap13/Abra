using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Player
    {
        //Колода карт игрока
        public List<Card> mDeck;
        //Имя игрока
        public string mName;
        
        public Player()
        {
            mDeck = new List<Card>();
        }

        public Player(string name)
        {
            mDeck = new List<Card>();
            mName = name;
        }
        
        public void PlayCard(GameField gameField, int choosedCard)
        {
            if(gameField.tableDeck.Contains(mDeck[choosedCard]) &&
                (mDeck[choosedCard].mValue == 1))
            {

            }
        }

        public void DropCard(GameField gameField)
        {

        }

        public void TellColor(GameField gameField)
        {

        }

        public void TellRank(GameField gameField)
        {

        }
    }
}
