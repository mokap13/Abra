using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanabi
{
    class Card
    {
        private CardColor mColor;
        private CardValue mValue;
        private string mName;

        public Card(CardColor color,CardValue value,string name)
        {
            mColor = color;
            mValue = value;
            mName = name;
        }

        public CardColor Color
        {
            get
            {
                return mColor;
            }
        }
        public CardValue Value
        {
            get
            {
                return mValue;
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
    enum CardValue
    {
        Zero = '0',
        One = '1', 
        Two = '2', 
        Three = '3', 
        Four = '4', 
        Five = '5'
    }
}
