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
        private int mChoosedCard;
        private int[] mChoosedCards;
        private CardColor? mCardColor;
        private CardRank? mCardRank;

        public Command(CommandName? commandName,int choosedCard)
        {
            mCommandName = commandName;
            mChoosedCard = choosedCard;
        }

        public Command(CommandName? commandName,int choosedCard, int[] choosedCards, CardColor? color)
        {
            mCommandName = commandName;
            mCardColor = color;
            mChoosedCards = choosedCards;
        }

        public Command(CommandName? commandName, int choosedCard, int[] choosedCards, CardRank? rank)
        {
            mCommandName = commandName;
            mCardRank = rank;
            mChoosedCards = choosedCards;
        }

        public Command(CommandName? commandName, int choosedCard, int[] choosedCards, CardColor? color, CardRank? rank)
        {
            mCommandName = commandName;
            mChoosedCard = choosedCard;
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

        public int ChoosedCard
        {
            get
            {
                return mChoosedCard;
            }
        }

        public int[] ChoosedCards
        {
            get
            {
                return mChoosedCards;
            }
        }

        public CardColor? CardColor
        {
            get
            {
                return mCardColor;
            }
        }

        public CardRank? CardRank
        {
            get
            {
                return mCardRank;
            }
        }
    }

    public enum CommandName
    {
        Playcard,
        Dropcard,
        Tellcolor,
        Tellrank
    }
}
