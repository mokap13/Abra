using System;
using System.Collections.Generic;

namespace hanabiabra
{
    class GameEngine
    {
        private const int START_PLAYER_DECK_SIZE = 5;
        private GameField mGameField;
        private Player mPlayerA;
        private Player mPlayerB;

        public GameEngine()
        {
            mGameField = new GameField();

            mPlayerA = new Player();
            mPlayerB = new Player();
            mGameField.currentPlayer = mPlayerA;
            mGameField.nextPlayer = mPlayerB;

            mGameField.UpdatePlayerStatus();
        }
        /// <summary>
        /// Подготавливает игру перед ходами игроков
        /// </summary>
        public void StartGame(Command command)
        {
            if (CheckPlayerActionStart(command.Deck))
            {
                mGameField.mainDeck = command.Deck;
                GiveStartDeck(START_PLAYER_DECK_SIZE, mPlayerA, mPlayerB);
                Output.ShowGameStatus(mGameField);
            }
            else
            {
                throw new Exception("Deck is incorrect");
            }
        }
        /// <summary>
        /// Раздает игрокам по стартовой колоде
        /// </summary>
        /// <param name="deckSize">Размер стартовой колоды</param>
        /// <param name="player">Игроки</param>
        private void GiveStartDeck(int deckSize, params Player[] player)
        {
            for (int i = 0; i < player.Length; i++)
            {
                for (int j = 0; j < deckSize; j++)
                {
                    player[i].TakeCardFromDeck(mGameField.mainDeck);
                } 
            }
        }
        /// <summary>
        /// Начинает игру
        /// </summary>
        public void MoveMake(Command command)
        {
            if (!TryExecuteCommand(command, mGameField)
                || mGameField.mainDeck.Count == 0)
            {
                mGameField.finished = true;
            }
            
            UpdateGameField(mGameField);
            Output.ShowGameStatus(mGameField);
        }
        /// <summary>
        /// Возвращает true, если действие игрока не нарушает правил игры и 
        /// false если при выполнении правила нарушаются
        /// </summary>
        private bool TryExecuteCommand(Command command, GameField gameField)
        {
            Player currentPlayer = gameField.currentPlayer;
            Player nextPlayer = gameField.nextPlayer;
            Deck tableDeck = gameField.tableDeck;
            //Card choosedCard = currentPlayer.Deck[command.CardIndex];

            switch (command.Name)
            {
                case "Play":
                    if (CheckPlayerActionPlayCard(currentPlayer.Deck[command.CardIndex], tableDeck))
                    {
                        if (CheckRisk(currentPlayer.Deck[command.CardIndex], tableDeck)
                            && gameField.finished == false)
                            gameField.risk++;
                        currentPlayer.PlayCard(command.CardIndex, gameField.tableDeck);
                        currentPlayer.TakeCardFromDeck(gameField.mainDeck);
                        mGameField.score++;
                        return true;
                    }
                    return false;
                case "Drop":
                    if (CheckPlayerActionDropCard(currentPlayer.Deck[command.CardIndex], tableDeck))
                    {
                        currentPlayer.DropCard(command.CardIndex);
                        currentPlayer.TakeCardFromDeck(gameField.mainDeck);
                        return true;
                    }
                    return false;
                case "color":
                    if (CheckPlayerActionTellColor(command, nextPlayer.Deck))
                    {
                        currentPlayer.TellColor(command, nextPlayer.Deck);
                        return true;
                    }
                    return false;
                case "rank":
                    if (CheckPlayerActionTellRank(command, nextPlayer.Deck))
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
        private void UpdateGameField(GameField gameField)
        {
            gameField.turn++;
            ChangePlayer(mGameField);
        }
        /// <summary>
        /// Меняет игроков местами(передает ход следующему)
        /// </summary>
        /// <param name="gameField"></param>
        private void ChangePlayer(GameField gameField)
        {
            Player tempPlayer = gameField.currentPlayer;
            gameField.currentPlayer = gameField.nextPlayer;
            gameField.nextPlayer = tempPlayer;
            gameField.UpdatePlayerStatus();
        }
        /// <summary>
        /// Возвращает true если ход рискованный, иначе false
        /// </summary>
        /// <param name="choosedCard">Выбранная карта</param>
        /// <param name="tableDeck">Колода стола</param>
        /// <returns></returns>
        private bool CheckRisk(Card choosedCard, Deck tableDeck)
        {
            List<CardColor> tableNoColors = new List<CardColor>();
            tableNoColors = tableDeck.GetNoColorsForRank(choosedCard.Rank - 1);

            if (choosedCard.CardVisible)
                return false;

            if (!choosedCard.RankVisible)
                return true;

            foreach (CardColor color in tableNoColors)
            {
                if (!choosedCard.NoColors.Contains(color))
                    return true;
                else
                    continue;
            }
            return false;
        }
        /// <summary>
        /// Возвращает true если основная колода удовлетворяет условиям игры
        /// </summary>
        /// <param name="mainDeck">Основная колода</param>
        private bool CheckPlayerActionStart(Deck mainDeck)
        {
            if (mainDeck.Count > 10)
                return true;
            return false;
        }
        /// <summary>
        /// Возвращает true если карту можно разыграть, иначе false
        /// </summary>
        private bool CheckPlayerActionPlayCard(Card choosedCard, Deck tableDeck)
        {
            if (choosedCard.Rank - 1 == tableDeck.GetMaxRank(choosedCard.Color))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если карту можно сбросить, иначе false
        /// </summary>
        private bool CheckPlayerActionDropCard(Card choosedCard, Deck tableDeck)
        {
            return true;
        }
        /// <summary>
        /// Возвращает true если можно сказать опоненту цвет выбранных карт
        /// </summary>
        private bool CheckPlayerActionTellColor(Command command, Deck nextPlayerDeck)
        {
            int[] cardIndexes = command.CardIndexes;
            CardColor cardColor = command.CardColor;
            //Проверяем кол-во указанных карт заданного цвета и кол-во имеющихся карт заданного цвета
            if (cardIndexes.Length != nextPlayerDeck.GetCountCardForColor(cardColor))
                return false;
            //Проверяем соответствие заданного цвета и цвет имеющихся карт
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                if (cardColor != nextPlayerDeck.Cards[command.CardIndexes[i]].Color)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Возвращает true если можно сказать опоненту ранг выбранных карт
        /// </summary>
        private bool CheckPlayerActionTellRank(Command command, Deck nextPlayerDeck)
        {
            int[] cardIndexes = command.CardIndexes;
            int cardRank = command.CardRank;
            //Проверяем кол-во указанных карт заданного цвета и кол-во имеющихся карт заданного цвета
            if (cardIndexes.Length != nextPlayerDeck.GetCountCardForRank(cardRank))
                return false;
            //Проверяем соответствие заданного цвета и цвет имеющихся карт
            for (int i = 0; i < cardIndexes.Length; i++)
            {
                if (cardRank != nextPlayerDeck.Cards[command.CardIndexes[i]].Rank)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Возвращает true если игра закончена
        /// </summary>
        public bool Finished
        {
            get
            {
                return mGameField.finished;
            }
        }
    }
}
