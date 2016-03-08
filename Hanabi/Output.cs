﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    static class Output
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
                System.Console.Write(card.Name + delimiter);
            }
            Console.Write("\n");
        }

        public static void ShowGameStatus(GameField gameField)
        {
            const char DELIMITER = ' ';
            int turn = gameField.turn;
            int score = gameField.score;
            int risk = gameField.risk;
            bool finished = gameField.finished;

            Player currentPlayer = gameField.currentPlayer;
            Player nextPlayer = gameField.nextPlayer;

            if (gameField.finished == true)
            {
                Console.WriteLine("Turn: {0}, cards: {1}, with risk: {2}", turn, score, risk);
            }
            Console.WriteLine("Turn: {0}, Score: {1}, Finished: {2}", turn, score, finished);

            ShowDeck(currentPlayer.Deck,DELIMITER);
            ShowDeck(nextPlayer.Deck,DELIMITER);
            ShowDeck(gameField.tableDeck, DELIMITER);
        }

        public static void ShowGameOverStatus(GameField gameField)
        {
            const char DELIMITER = ' ';
            int turn = gameField.turn;
            int score = gameField.score;
            int risk = gameField.risk;
            bool finished = gameField.finished;

            Player currentPlayer = gameField.currentPlayer;
            Player nextPlayer = gameField.nextPlayer;

            Console.WriteLine("Turn: {0}, cards: {1}, with risk: {2}", turn, score, risk);
            Console.WriteLine("Turn: {0}, Score: {1}, Finished: {2}", turn, score, finished);

            ShowDeck(currentPlayer.Deck, DELIMITER);
            ShowDeck(nextPlayer.Deck, DELIMITER);
            ShowDeck(gameField.tableDeck, DELIMITER);
        }
    }
}