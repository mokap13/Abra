
namespace hanabiabra
{
    class Command
    {
        private string mName;
        private int[] mCardIndexes;
        private int mCardIndex;
        private CardColor mCardColor;
        private int mCardRank;
        private Deck mDeck;
        
        /// <param name="name">Имя команды</param>
        /// <param name="deck">Колода карт</param>
        public Command(string name, Deck deck)
        {
            mName = name;
            mDeck = deck;
        }
        
        /// <param name="name">Имя команды</param>
        /// <param name="cardIndex">Номер карты</param>
        public Command(string name, int cardIndex)
        {
            mName = name;
            mCardIndex = cardIndex;
        }
        
        /// <param name="name">Имя команды</param>
        /// <param name="cardIndexes">Номера карт</param>
        /// <param name="color">Цвет карты</param>
        public Command(string name, int[] cardIndexes, CardColor color)
        {
            mName = name;
            mCardColor = color;
            mCardIndexes = cardIndexes;
        }
        
        /// <param name="name">Имя команды</param>
        /// <param name="cardIndexes">Номера карт</param>
        /// <param name="rank">Ранг карты</param>
        public Command(string name, int[] cardIndexes, int rank)
        {
            mName = name;
            mCardRank = rank;
            mCardIndexes = cardIndexes;
        }

        public string Name
        {
            get
            {
                return mName;
            }
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
        
        public CardColor CardColor
        {
            get
            {
                return mCardColor;
            }
        }
        
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
        }
    }
}
