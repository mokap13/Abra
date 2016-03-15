using System.Collections.Generic;

namespace hanabiabra
{
    class Card
    {
        public const int CARD_COLORS_COUNT = 5;
        public const int CARD_RANKS_COUNT = 5;
        private int TRUE_CARD_VALUE = 1;
        private CardColor mColor;
        private int mRank;
        private string mName;
        private bool mColorVisible;
        private bool mRankVisible;
        private List<CardColor> mNoColors;
        private List<int> mNoRanks;

        public Card(string name)
        {
            mName = name;
            mColor = (CardColor)name[0];
            mRank = (int)char.GetNumericValue(name[1]);
            mNoColors = new List<CardColor>();
            mNoRanks = new List<int>();
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
                if (mNoColors.Count == CARD_COLORS_COUNT - TRUE_CARD_VALUE)
                    mColorVisible = true;

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
                if (mNoRanks.Count == CARD_RANKS_COUNT - 1)
                    return true;

                return mRankVisible;
            }
            set
            {
                mRankVisible = value;
            }
        }
        public bool CardVisible
        {
            get
            {
                if (ColorVisible && RankVisible)
                    return true;
                else
                    return false;
            }
        }
        public List<CardColor> NoColors
        {
            get
            {
                return mNoColors;
            }
        }
        public List<int> NoRanks
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
