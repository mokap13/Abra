using System.Collections.Generic;
using System.Linq;

namespace hanabiabra
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

        public Deck(List<Card> cards)
        {
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
        /// Возвращает колличество карт с указанным цветом
        /// </summary>
        /// <param name="cardColor">Цвет карты</param>
        /// <returns>Колличество карт с указанным цветом</returns>
        public int GetCountCardForColor(CardColor cardColor)
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
        /// Возвращает колличество карт с указанным рангом
        /// </summary>
        /// <param name="cardRank">Ранг карты</param>
        /// <returns>Колличество карт с указанным рангом</returns>
        public int GetCountCardForRank(int cardRank)
        {
            int count = 0;

            foreach (Card card in mCards)
            {
                if (card.Rank == cardRank)
                    count++;
            }
            return count;
        }
        /// <summary>
        /// Возвращает true если карты с указанными номерами имееют указанный цвет, иначе false
        /// </summary>
        /// <param name="cardColor">Цвет карты</param>
        /// <param name="cardIndexes">Индекс карты</param>
        public bool CheckColor(CardColor cardColor, int[] cardIndexes)
        {
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                if (cardColor != mCards[cardIndexes[i]].Color)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Возвращает true если карты с указанными номерами имееют указанный ранг, иначе false
        /// </summary>
        /// <param name="cardRank">Ранг карты</param>
        /// <param name="cardIndexes">Индекс карт</param>
        /// <returns></returns>
        public bool CheсkRank(int cardRank, int[] cardIndexes)
        {
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                if (cardRank != mCards[cardIndexes[i]].Rank)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Обновляет статус видимости цвета
        /// </summary>
        /// <param name="cardColor">Цвет карты</param>
        /// <param name="cardIndexes">Индексы карт</param>
        public void UpdateColorVisible(CardColor cardColor, int[] cardIndexes)
        {
            for (int i = 0; i < cardIndexes.Length; i++)
			{
                AddNoColor(cardColor, cardIndexes[i]);
                if (CompareCardColor(cardColor, cardIndexes[i]))
                    mCards[cardIndexes[i]].ColorVisible = true;
                
			}
        }
        /// <summary>
        /// Обновляет статус видимости ранга
        /// </summary>
        /// <param name="cardRank">Ранг карты</param>
        /// <param name="cardIndexes">Индексы карт</param>
        public void UpdateRankStatus(int cardRank, int[] cardIndexes)
        {
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                AddNoRank(cardRank, cardIndexes[i]);
                if (CompareCardRank(cardRank, cardIndexes[i]))
                    mCards[cardIndexes[i]].RankVisible = true;
            }
        }
        /// <summary>
        /// Извлекает карту из колоды по индексу
        /// </summary>
        /// <param name="cardIndex">Индекс карты в колоде</param>
        public Card PullIndexCard(int cardIndex)
        {
            Card pullCard = mCards[cardIndex];
            mCards.RemoveAt(cardIndex);
            return pullCard;
        }
        /// <summary>
        /// Заменяет в колоде карту, с идентичным цветом на выбранную карту 
        /// </summary>
        public void PushCardForColor(Card choosedCard)
        {
            if (mCards.Count == 0)
            {
                mCards.Add(choosedCard);
            }
            foreach (Card card in mCards)
            {
                if (card.Color == choosedCard.Color)
                {
                    int cardIndex = mCards.IndexOf(card);
                    mCards.Remove(card);
                    mCards.Insert(cardIndex, choosedCard);
                    break;
                }
            }
        }
        /// <summary>
        /// Получить максимальный ранг среди карт из колоды
        /// указанного цвета
        /// </summary>
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
        /// <summary>
        /// Возвращает список цветов не характерных для карт указанного ранга
        /// </summary>
        /// <param name="cardRank"></param>
        /// <returns></returns>
        public List<CardColor> GetNoColorsForRank(int cardRank)
        {
            List<CardColor> listColors = new List<CardColor>();
            foreach (Card card in mCards)
            {
                if (card.Rank != cardRank)
                {
                    listColors.Add(card.Color);
                }
            }
            return listColors;
        }
        
        public Card this[int index]
        {
            get { return mCards[index]; }
            set { mCards[index] = value; }
        }
        /// <summary>
        /// Возвращает кол-во карт в колоде
        /// </summary>
        public int Count
        {
            get
            {
                return mCards.Count;
            }
        }
        /// <summary>
        /// Возвращает true если указанный цвет соответствует цвету карты с указанным индексом,
        /// иначе false
        /// </summary>
        /// <param name="choosedColor">Цвет карты</param>
        /// <param name="cardIndex">Индекс карты в колоде</param>
        public bool CompareCardColor(CardColor choosedColor, int cardIndex)
        {
            if (choosedColor == mCards[cardIndex].Color)
                return true;

            return false;
        }
        /// <summary>
        /// Добавляет не характерный для карты цвет в соответствующий список карты с 
        /// указанным индексом
        /// </summary>
        /// <param name="choosedColor">Цвет карты</param>
        /// <param name="cardIndex">Индекс карты в колоде</param>
        public void AddNoColor(CardColor choosedColor, int cardIndex)
        {
            for (int i = 0; i < mCards.Count; i++)
			{
                if(i != cardIndex && !mCards[i].NoColors.Contains(choosedColor))
                    mCards[i].NoColors.Add(choosedColor);
			}
        }
        /// <summary>
        /// Возвращает true если указанный ранг соответствует рангу карты с указанным индексом,
        /// иначе false
        /// </summary>
        /// <param name="choosedColor">Ранг карты</param>
        /// <param name="cardIndex">Индекс карты в колоде</param>
        public bool CompareCardRank(int cardRank, int cardIndex)
        {
            if (cardRank == mCards[cardIndex].Rank)
                return true;

            return false;
        }
        /// <summary>
        /// Добавляет не характерный для карты ранг в соответствующий список карты с 
        /// указанным индексом
        /// </summary>
        /// <param name="choosedColor">Ранг карты</param>
        /// <param name="cardIndex">Индекс карты в колоде</param>
        public void AddNoRank(int choosedRank, int cardIndex)
        {
            for (int i = 0; i < mCards.Count; i++)
            {
                if (i != cardIndex && !mCards[i].NoRanks.Contains(choosedRank))
                    mCards[i].NoRanks.Add(choosedRank);
            }
        }
    }
}
