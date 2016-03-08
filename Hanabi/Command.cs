﻿using System;
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
        private CardRank? mCardRank;

        public Command(CommandName? commandName)
        {
            mCommandName = commandName;
        }

        public Command(CommandName? commandName, int[] choosedCards, CardColor? color)
        {
            mCommandName = commandName;
            mCardColor = color;
            mChoosedCards = choosedCards;
        }

        public Command(CommandName? commandName, int[] choosedCards, CardRank? rank)
        {
            mCommandName = commandName;
            mCardRank = rank;
            mChoosedCards = choosedCards;
        }

        public Command(CommandName? commandName, int[] choosedCards, CardColor? color, CardRank? rank)
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
        public CardRank? CardRank
        {
            get
            {
                return mCardRank;
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
        Tellrank
    }
}
