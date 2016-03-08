using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Deck
    {
        private string mName;

        private List<Card> mCards;

        public Deck()
        {
            mCards = new List<Card>();
        }

        public Deck(string name, List<Card> cards)
        {
            mName = name;
            mCards = cards;
        }

        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        public List<Card> Cards
        {
            get
            {
                return mCards;
            }
        }

        /// <summary>
        /// Возвращает колличество карт с заданным цветом
        /// </summary>
        /// <param name="cardColor">Цвет карты</param>
        /// <returns>Колличество карт с заданным цветом</returns>
        public int GetNumberColor(CardColor? cardColor)
        {
            int count = 0;

            foreach (Card card in mCards)
            {
                if (card.Color == cardColor)
                    count++;
            }
            return count;
        }
        /// <summary>
        /// Возвращает колличество карт с заданным рангом
        /// </summary>
        /// <param name="cardRank">Ранг карты</param>
        /// <returns>Колличество карт с заданным рангом</returns>
        public int GetNumberRank(int? cardRank)
        {
            int count = 0;

            foreach (Card card in mCards)
            {
                if (card.Rank == cardRank)
                    count++;
            }
            return count;
        }

        public bool CheckColor(CardColor? cardColor,int[] choosedCards)
        {
            for (int i = 0; i < choosedCards.Length; i++)
            {
                if (cardColor != mCards[choosedCards[i]].Color)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheсkRank(int? cardRank, int[] choosedCards)
        {
            for (int i = 0; i < choosedCards.Length; i++)
            {
                if (cardRank != mCards[choosedCards[i]].Rank)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ContainCard(Card choosedCard)
        {
            foreach (Card card in mCards)
            {
                if (card.Color == choosedCard.Color && card.Rank == choosedCard.Rank)
                    return true;
            }
            return false;
        }

        public Card PullTopCard()
        {
            const int INDEX_TOP_CARD = 0;

            Card pullCard = mCards.First();
            mCards.RemoveAt(INDEX_TOP_CARD);
            return pullCard;
        }

        public Card PullIndexCard(int cardIndex)
        {
            Card pullCard = mCards[cardIndex];
            mCards.RemoveAt(cardIndex);
            return pullCard;
        }

        public void PushCardForColor(Card choosedCard)
        {
            foreach (Card card in mCards)
            {
                if (card.Color == choosedCard.Color)
                {
                    int thisCardIndex = mCards.IndexOf(card);
                    mCards.Remove(card);
                    mCards.Insert(thisCardIndex, choosedCard);
                    break;
                }
            }
        }

        public int GetMaxRank(CardColor cardColor)
        {
            int count = 0;
            foreach (Card card in mCards)
            {
                if (card.Color == cardColor && (int)card.Rank > count)
                {
                    count = card.Rank;
                }
            }
            return count;
        }
    }
}
