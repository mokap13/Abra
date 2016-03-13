using System.Linq;

namespace hanabiabra
{
    class Player
    {
        private Deck mDeck;
        private Deck mDropDeck;

        public Player()
        {
            mDeck = new Deck();
            mDropDeck = new Deck();
        }

        public Deck Deck
        {
            get
            {
                return mDeck;
            }
        }

        public Deck DropDeck
        {
            get
            {
                return mDropDeck;
            }
            set
            {
                mDropDeck = value;
            }
        }
        /// <summary>
        /// Игрок перекладывает карту из указанной колоды в 
        /// свою колоду
        /// </summary>
        public void TakeCardFromDeck(Deck sourceDeck)
        {
            mDeck.Cards.Add(sourceDeck.Cards.First());
            sourceDeck.Cards.RemoveAt(0);
        }
        /// <summary>
        /// Игрок разыгрывает карту с указанным индексом
        /// </summary>
        public void PlayCard(int cardIndex, Deck tableDeck)
        {
            Card pullCard = mDeck.PullIndexCard(cardIndex);
            tableDeck.PushCardForColor(pullCard);
        }
        /// <summary>
        /// Игрок сбрасывает карту с указанным индексом
        /// </summary>
        public void DropCard(int cardIndex)
        {
            Card pullCard = mDeck.PullIndexCard(cardIndex);
            mDropDeck.PushCardForColor(pullCard);
        }
        /// <summary>
        /// Игрок раскрывает опоненту цвет указанных карт
        /// </summary>
        public void TellColor(Command command, Deck nextPlayerDeck)
        {
            nextPlayerDeck.UpdateColorVisible(command.CardColor, command.CardIndexes);
        }
        /// <summary>
        /// Игрок раскрывает опоненту ранг указанных карт
        /// </summary>
        public void TellRank(Command command, Deck nextPlayerDeck)
        {
            nextPlayerDeck.UpdateRankStatus(command.CardRank, command.CardIndexes);
        }
    }
}
