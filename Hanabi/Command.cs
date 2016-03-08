using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Command
    {
        private CommandName? mCommandName;
        private int[] mChoosedCards;
        private CardColor? mCardColor;
        private int? mCardRank;
        private Deck mDeck;

        public Command(CommandName? commandName, Deck deck)
        {
            mCommandName = commandName;
            mDeck = deck;
        }

        public Command(CommandName? commandName, int[] choosedCards, CardColor? color)
        {
            mCommandName = commandName;
            mCardColor = color;
            mChoosedCards = choosedCards;
        }

        public Command(CommandName? commandName, int[] choosedCards, int? rank)
        {
            mCommandName = commandName;
            mCardRank = rank;
            mChoosedCards = choosedCards;
        }

        public Command(CommandName? commandName, int[] choosedCards, CardColor? color, int? rank)
        {
            mCommandName = commandName;
            mChoosedCards = choosedCards;
            mCardRank = rank;
            mCardColor = color;
        }

        public CommandName? CommandName
        {
            get
            {
                return mCommandName;
            }
        }

        public int[] ChoosedCards
        {
            get
            {
                return mChoosedCards;
            }
        }
        /// <summary>
        /// Цвет карты
        /// </summary>
        public CardColor? CardColor
        {
            get
            {
                return mCardColor;
            }
        }
        /// <summary>
        /// Ранг карты
        /// </summary>
        public int? CardRank
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
                return Deck;
            }
            set
            {
                Deck = value;
            }
        }
    }
    /// <summary>
    /// Имя команды
    /// </summary>
    public enum CommandName
    {
        Playcard,
        Dropcard,
        Tellcolor,
        Tellrank,
        Startnew
    }
}
