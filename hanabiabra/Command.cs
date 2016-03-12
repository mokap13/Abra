using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanabiabra
{
    class Command
    {
        private int[] mCardIndexes;
        private int mCardIndex;
        private CardColor mCardColor;
        private int mCardRank;
        private Deck mDeck;
        private string mName;

        public Command(Deck deck)
        {
            mDeck = deck;
        }

        public Command(string name, Deck deck)
        {
            mName = name;
            mDeck = deck;
        }

        public Command(string Name, int cardIndex)
        {
            mName = Name;
            mCardIndex = cardIndex;
        }

        public Command(string Name, int[] choosedCards, CardColor color)
        {
            mName = Name;
            mCardColor = color;
            mCardIndexes = choosedCards;
        }

        public Command(string Name, int[] choosedCards, int rank)
        {
            mName = Name;
            mCardRank = rank;
            mCardIndexes = choosedCards;
        }

        public int[] CardIndexes
        {
            get
            {
                return mCardIndexes;
            }
        }

        public int CardIndex
        {
            get
            {
                return mCardIndex;
            }
        }
        /// <summary>
        /// Цвет карты
        /// </summary>
        public CardColor CardColor
        {
            get
            {
                return mCardColor;
            }
        }
        /// <summary>
        /// Ранг карты
        /// </summary>
        public int CardRank
        {
            get
            {
                return mCardRank;
            }
        }

        public Deck Deck
        {
            get
            {
                return mDeck;
            }
            set
            {
                mDeck = value;
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
}
