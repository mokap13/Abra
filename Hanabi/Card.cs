using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanabi
{
    class Card
    {
        private CardColor mColor;
        private int mRank;
        private string mName;

        public Card(CardColor color,int rank,string name)
        {
            mColor = color;
            mRank = rank;
            mName = name;
        }

        public Card(char color, char rank)
        {
            mColor = (CardColor)color;
            mRank = (int)Char.GetNumericValue(rank);
            mName = String.Concat(color, rank);
        }

        public CardColor Color
        {
            get
            {
                return mColor;
            }
        }
        public int Rank
        {
            get
            {
                return mRank;
            }
            set
            {
                if (value <= 5 && value >= 0)
                {
                    Rank = value;
                }
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
}
