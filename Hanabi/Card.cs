using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanabi
{
    class Card
    {
        private CardColor mColor;
        private CardRank mRank;
        private string mName;

        public Card(CardColor color,CardRank rank,string name)
        {
            mColor = color;
            mRank = rank;
            mName = name;
        }

        public Card(char color, char rank)
        {
            mColor = (CardColor)color;
            mRank = (CardRank)rank;
            mName = String.Concat(color, rank);
        }

        public CardColor Color
        {
            get
            {
                return mColor;
            }
        }
        public CardRank Rank
        {
            get
            {
                return mRank;
            }
        }
        public string Name
        {
            get
            {
                return mName;
            }
        }
    }
    enum CardColor
    {
        Red = 'R',
        Green = 'G',
        Blue = 'B',
        Yellow = 'Y',
        White = 'W'
    }
    enum CardRank
    {
        Zero,// = '0',
        One,// = '1', 
        Two,// = '2', 
        Three,// = '3', 
        Four,// = '4', 
        Five// = '5'
    }
}
