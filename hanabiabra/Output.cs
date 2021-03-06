﻿using System;

namespace hanabiabra
{
    class Output
    {
        /// <summary>
        /// Выводит в консоль последовательно имена карт, через заданный разделитель
        /// </summary>
        /// <param name="deck">Колода карт</param>
        /// <param name="delimiter">Разделитель</param>
        public static void ShowDeck(Deck deck, char delimiter)
        {
            Console.Write("{0,18}", deck.Name);
            foreach (var card in deck.Cards)
            {
                Console.Write(card.Name + delimiter);
            }
        }
        /// <summary>
        /// Выводит в консоль последовательно видимые свойства карт, через заданный разделитель
        /// </summary>
        /// <param name="deck">Колода карт</param>
        /// <param name="delimiter">Разделитель</param>
        public static void ShowDeckIf(Deck deck, char delimiter)
        {
            Console.Write("{0,18}", deck.Name);
            foreach (var card in deck.Cards)
            {
                if (card.ColorVisible == true)
                    Console.Write(card.Color.ToString()[0]);
                else
                    Console.Write("*");
                if (card.RankVisible == true)
                    Console.Write(card.Rank.ToString());
                else
                    Console.Write("*");
                Console.Write(delimiter);
            }
        }
        /// <summary>
        /// Выводит в консоль параметры игрового поля
        /// </summary>
        public static void ShowGameStatus(GameField gameField)
        {
            int turn = gameField.turn;
            int score = gameField.score;
            int risk = gameField.risk;
            bool finished = gameField.finished;
            
            Player currentPlayer = gameField.currentPlayer;
            Player nextPlayer = gameField.nextPlayer;

            if (gameField.finished)
            {
                Console.WriteLine("Turn: {0}, cards: {1}, with risk: {2}", turn, score, risk);
            }

            //Console.WriteLine("Turn: {0}, Score: {1}, Finished: {2}", turn, score, finished);
            //ShowDeckIf(currentPlayer.Deck, DELIMITER);
            //Console.Write(" ---- ");
            //ShowDeckIf(currentPlayer.DropDeck, DELIMITER);
            //Console.Write("\n");
            //ShowDeck(nextPlayer.Deck, DELIMITER);
            //Console.Write("\n");
            //ShowDeck(gameField.tableDeck, DELIMITER);
            //Console.Write("\n");
            //Console.WriteLine("-----------------------------------------------------");
        }
    }
}
