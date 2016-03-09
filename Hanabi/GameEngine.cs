using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// <summary>
        /// Начинает игру
        /// </summary>
        public void StartGame(string[] sourceData, ref int j)
        {
            mGameField.mainDeck = Input.ReadMainDeck(mGameField, sourceData[j]);
            j++;
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

            while (mGameField.finished == false)
            {
                Output.ShowGameStatus(mGameField);
                mCommand = Input.ReadCommand(sourceData[j]);

                if (mGameField.finished == false && TryExecuteCommand(mGameField, mCommand) == true && mGameField.mainDeck.Count > 0)
                {
                    MakeMove(mGameField);
                }
                else
                {
                    MakeMove(mGameField);
                    mGameField.finished = true;
                    Output.ShowGameStatus(mGameField);
                }
                j++;
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
                #region Playcard
                case CommandName.Playcard:
                    choosedCard = gameField.currentPlayer.Deck.Cards[command.ChoosedCards[0]];
                    //Ранг выбранной карты должен быть на 1 выше ранга карты колоды стола
                    
                    if ((int)choosedCard.Rank - 1 == gameField.tableDeck.GetMaxRank(choosedCard.Color))
                    {
                        if (CheckRisk(gameField, choosedCard) == true)
                        {
                            gameField.risk++;
                        }
                            
                        gameField.currentPlayer.PlayCard(gameField, command);
                        gameField.score++;
                        return true;
                    }
                    else
                    {
                        gameField.currentPlayer.DropCard(gameField, command);
                        return false;
                    } 
                #endregion
                #region Dropcard
                case CommandName.Dropcard:
                    choosedCard = gameField.currentPlayer.Deck.Cards[command.ChoosedCards[0]];
                    gameField.currentPlayer.DropCard(gameField, command);
                    return true;
                #endregion
                #region Tellcolor
                case CommandName.Tellcolor:
                    int numberCardsOneColor = gameField.nextPlayer.Deck.GetNumberColor(command.CardColor);
                    if (numberCardsOneColor != command.ChoosedCards.Length)
                        return false;
                    if (gameField.nextPlayer.Deck.CheckColor(command.CardColor, command.ChoosedCards) == false)
                        return false;
                    gameField.nextPlayer.Deck.ChangeStatusColorVisible(command.CardColor, command.ChoosedCards);
                    return true;
                #endregion
                #region Tellrank
                case CommandName.Tellrank:
                    int numberCardsOneRank = gameField.nextPlayer.Deck.GetNumberRank(command.CardRank);
                    if (numberCardsOneRank != command.ChoosedCards.Length)
                        return false;
                    if (gameField.nextPlayer.Deck.CheсkRank(command.CardRank, command.ChoosedCards) == false)
                        return false;
                    gameField.nextPlayer.Deck.ChangeStatusRankVisible(command.CardRank, command.ChoosedCards);
                    return true;
                #endregion
                default:
                    return false;
            }
        }
        /// <summary>
        /// Обновляет параметры игрового поля
        /// </summary>
        /// <param name="gameField">Исходное поле</param>
        private void MakeMove(GameField gameField)
        {
            gameField.turn++;
            Player tempPlayer = gameField.currentPlayer;
            gameField.currentPlayer = gameField.nextPlayer;
            gameField.nextPlayer = tempPlayer;
            gameField.UpdatePlayerStatus();
        }

        private bool CheckRisk(GameField gameField, Card card)
        {
            List<CardColor?> tableNoColors = new List<CardColor?>();
            tableNoColors = gameField.tableDeck.GetNoColorsForRank(card.Rank - 1);
            
            if (gameField.finished == true)
                return false;

            if (card.CardVisible == true)
                return false;

            if (card.RankVisible == false)
            {
                return true;
            }

            if (card.ColorVisible == true)
            {
                if (gameField.tableDeck.GetMaxRank(card.Color) == 4)
                    return false; 
            }

            //if (gameField.tableDeck.GetMinRank() == gameField.tableDeck.GetMaxRank())
            //{
            //    return false;
            //}
            foreach (CardColor? color in tableNoColors)
            {
                if (card.NoColors.Contains(color) == false)
                {
                    return true;
                }
                else
                {
                    continue;
                }
            } 
            return false;
        }
    }
}
