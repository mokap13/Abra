﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class GameEngine
    {
        private const int START_SIZE_PLAYER_DECK = 5;
        private Command mCommand;
        private GameField mGameField;
        private Player mPlayerA;
        private Player mPlayerB;
        private Deck mMainDeck;
        private Deck mTableDeck;

        public GameEngine()
        {
            mGameField = new GameField();

            mPlayerA = new Player();
            mPlayerB = new Player();
            mGameField.currentPlayer = mPlayerA;
            mGameField.nextPlayer = mPlayerB;
            mMainDeck = mGameField.mainDeck;
            mTableDeck = mGameField.tableDeck;

            mGameField.UpdatePlayerStatus();
        }

        public void StartGame()
        {
            mGameField.mainDeck = Input.ReadMainDeck(mGameField);

            #region Игроки берут по 5 карт из основной колоды
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                mPlayerA.TakeCardFromDeck(mGameField.mainDeck);
            }
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                mPlayerB.TakeCardFromDeck(mGameField.mainDeck);
            }
            #endregion

            while (true)
            {
                Output.ShowGameStatus(mGameField);
                mCommand = Input.ReadCommand();

                if (mGameField.finished == false && TryExecuteCommand(mGameField, mCommand) == true)
                {
                    MakeMove(mGameField);
                }
                else
                {
                    mGameField.finished = true;
                }
            }
        }
        /// <summary>
        /// Возвращает true, если действие игрока не нарушает правил игры и 
        /// false если при выполнении правила нарушаются
        /// </summary>
        /// <param name="gameField"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool TryExecuteCommand(GameField gameField, Command command)
        {
            Card choosedCard;
            switch (command.CommandName)
            {
                case CommandName.Playcard:
                    choosedCard = gameField.currentPlayer.Deck.Cards[command.ChoosedCards[0]];
                    //Ранг выбранной карты должен быть на 1 выше ранга карты колоды стола
                    if ((int)choosedCard.Rank - 1 == gameField.tableDeck.GetMaxRank(choosedCard.Color))
                    {
                        gameField.currentPlayer.PlayCard(gameField, command);
                        gameField.currentPlayer.TakeCardFromDeck(gameField.mainDeck);
                        gameField.score++;
                        if (gameField.currentPlayer.Deck.Cards[command.ChoosedCards[0]].CardVisible == false)
                        {
                            gameField.risk++;
                        }
                        return true;
                    }
                    else
                    {
                        gameField.currentPlayer.DropCard(gameField, command);
                        return false;
                    }
                case CommandName.Dropcard:
                    choosedCard = gameField.currentPlayer.Deck.Cards[command.ChoosedCards[0]];
                    gameField.currentPlayer.DropCard(gameField, command);
                    return true;
                case CommandName.Tellcolor:
                    int numberCardsOneColor = gameField.nextPlayer.Deck.GetNumberColor(command.CardColor);
                    if (numberCardsOneColor != command.ChoosedCards.Length)
                        return false;
                    if(gameField.nextPlayer.Deck.CheckColor(command.CardColor, command.ChoosedCards) == false)
                        return false;
                    gameField.nextPlayer.Deck.ChangeStatusColorVisible(command.CardColor, command.ChoosedCards);
                    return true;
                case CommandName.Tellrank:
                    int numberCardsOneRank = gameField.nextPlayer.Deck.GetNumberRank(command.CardRank);
                    if (numberCardsOneRank != command.ChoosedCards.Length)
                        return false;
                    if(gameField.nextPlayer.Deck.CheсkRank(command.CardRank, command.ChoosedCards) == false)
                        return false;
                    gameField.nextPlayer.Deck.ChangeStatusRankVisible(command.CardRank, command.ChoosedCards);
                    return true;
                default:
                    return false;
            }
            return false;
        }

        private void MakeMove(GameField gameField)
        {
            gameField.turn++;
            Player tempPlayer = gameField.currentPlayer;
            gameField.currentPlayer = gameField.nextPlayer;
            gameField.nextPlayer = tempPlayer;
            gameField.UpdatePlayerStatus();
        }
    }
}
