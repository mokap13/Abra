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
        private bool mColorVisible;
        private bool mRankVisible;
        private List<CardColor?> mNoColors;
        private List<int?> mNoRanks;

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
            mColorVisible = false;

            mNoColors = new List<CardColor?>();
            mNoRanks = new List<int?>();
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
        public bool ColorVisible
        {
            get
            {
                return mColorVisible;
            }
            set
            {
                mColorVisible = value;
            }
        }
        public bool RankVisible
        {
            get
            {
                return mRankVisible;
            }
            set
            {
                mRankVisible = true;
            }
        }
        public bool CardVisible
        {
            get
            {
                if (mColorVisible == true && mRankVisible == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<CardColor?> NoColors
        {
            get
            {
                return mNoColors;
            }
        }
        public List<int?> NoRanks
        {
            get
            {
                return mNoRanks;
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
