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
        public void StartGame(string[] sourceData)
        {
            int i = 0;
            while (mGameField.finished == false)
            {
                Output.ShowGameStatus(mGameField);
                mCommand = Input.ParseCommand(sourceData[i]);
                
                if (mGameField.finished == false
                    && TryExecuteCommand(mCommand,mGameField) == true
                    && mGameField.mainDeck.Count > 0)
                {
                    MakeMove(mGameField);
                }
                else
                {
                    MakeMove(mGameField);
                    mGameField.finished = true;
                    Output.ShowGameStatus(mGameField);
                }
                i++;
            }
        }
        /// <summary>
        /// Возвращает true, если действие игрока не нарушает правил игры и 
        /// false если при выполнении правила нарушаются
        /// </summary>
        /// <param name="gameField"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool TryExecuteCommand(Command command, GameField gameField)
        {
            Player currentPlayer = gameField.currentPlayer;
            Player nextPlayer = gameField.nextPlayer;
            Deck tableDeck = gameField.tableDeck;
            //Card choosedCard = currentPlayer.Deck[command.CardIndex];

            switch (command.Name)
            {
                case "deck":
                    if (IsValidStart(command.Deck))
                    {
                        gameField.mainDeck = command.Deck;
                        TakeStartDeck();
                        return true;
                    }
                    return false;
                case "Play":
                    if (IsValidPlayCard(currentPlayer.Deck[command.CardIndex], tableDeck))
                    {
                        if (CheckRisk(currentPlayer.Deck[command.CardIndex], tableDeck)
                            &&gameField.finished == false)
                                gameField.risk++;
                        currentPlayer.PlayCard(command.CardIndex, gameField.tableDeck);
                        currentPlayer.TakeCardFromDeck(gameField.mainDeck);
                        return true;
                    }
                    return false;
                case "Drop":
                    if (IsValidDropCard(currentPlayer.Deck[command.CardIndex], tableDeck))
                    {
                        currentPlayer.DropCard(command.CardIndex);
                        currentPlayer.TakeCardFromDeck(gameField.mainDeck);
                        return true;
                    }
                    return false;
                case "color":
                    if (IsValidTellColor(command, nextPlayer.Deck))
                    {
                        currentPlayer.TellColor(command, nextPlayer.Deck);
                        return true;
                    }
                    return false;
                case "rank":
                    if (IsValidTellRank(command, nextPlayer.Deck))
                    {
                        currentPlayer.TellRank(command, nextPlayer.Deck);
                        return true;
                    }
                    return false;
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

        private bool CheckRisk(Card choosedCard, Deck tableDeck)
        {
            List<CardColor> tableNoColors = new List<CardColor>();
            tableNoColors = tableDeck.GetNoColorsForRank(choosedCard.Rank - 1);

            //if (gameField.finished == true)
            //    return false;

            if (choosedCard.CardVisible == true)
                return false;

            if (choosedCard.RankVisible == false)
            {
                return true;
            }

            foreach (CardColor color in tableNoColors)
            {
                if (choosedCard.NoColors.Contains(color) == false)
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

        private bool IsValidStart(Deck mainDeck)
        {
            if (mainDeck.Count > 10)
                return true;
            return false;
        }

        private bool IsValidPlayCard(Card choosedCard, Deck tableDeck)
        {
            if (choosedCard.Rank - 1 == tableDeck.GetMaxRank(choosedCard.Color))
                return true;

            return false;
        }

        private bool IsValidDropCard(Card choosedCard, Deck tableDeck)
        {
            return true;
        }

        private bool IsValidTellColor(Command command, Deck nextPlayerDeck)
        {
            int[] cardIndexes = command.CardIndexes;
            CardColor cardColor = command.CardColor;
            //Проверяем кол-во указанных карт заданного цвета и кол-во имеющихся карт заданного цвета
            if (cardIndexes.Length != nextPlayerDeck.GetCountCardForColor(cardColor))
                return false;
            //Проверяем соответствие заданного цвета и цвет имеющихся карт
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                if (cardColor != nextPlayerDeck.Cards[i].Color)
                    return false;
            }
            return true;
        }

        private bool IsValidTellRank(Command command, Deck nextPlayerDeck)
        {
            int[] cardIndexes = command.CardIndexes;
            int cardRank = command.CardRank;
            //Проверяем кол-во указанных карт заданного цвета и кол-во имеющихся карт заданного цвета
            if (cardIndexes.Length != nextPlayerDeck.GetCountCardForRank(cardRank))
                return false;
            //Проверяем соответствие заданного цвета и цвет имеющихся карт
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                if (cardRank != nextPlayerDeck.Cards[i].Rank)
                    return false;
            }
            return true;
        }

        private void TakeStartDeck()
        {
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                mPlayerA.TakeCardFromDeck(mGameField.mainDeck);
            }
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                mPlayerB.TakeCardFromDeck(mGameField.mainDeck);
            }
        }
    }
}
