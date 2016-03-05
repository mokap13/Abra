using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    static class Output
    {
        public static void ShowDeck(List<Card> deck)
        {
            foreach (var card in deck)
            {
                System.Console.Write(card.Name + " "); 
            }
            Console.Write("\n");
        }

        public static void ShowDeck(List<Card> deck, string name)
        {
            Console.Write("{0,18}", name);
            foreach (var card in deck)
            {
                Console.Write(card.Name + " ");
            }
            Console.Write("\n");
        }

        public static void ShowGameStatus(GameField gameField)
        {
            int turn = gameField.turn;
            int score = gameField.score;
            bool finished = gameField.finished;

            Player currentPlayer = gameField.currentPlayer;
            Player nextPlayer = gameField.nextPlayer;

            Console.WriteLine("turn: {0}, score: {1}, finished: {2}", turn, score, finished);

            ShowDeck(currentPlayer.Deck, "Current player: ");
            ShowDeck(nextPlayer.Deck, "Next player: ");
            ShowDeck(gameField.tableDeck, "Table: ");
        }
    }
}