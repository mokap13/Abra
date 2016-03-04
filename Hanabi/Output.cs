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
                System.Console.Write(card.Name); 
                Console.Write(" ");
            }
            Console.Write("\n");
        }
        
        public static void ShowStatus(GameField gameField)
        {
            const int EMPTY_SPACE = 18;
            int Turn = gameField.turn;
            int Score = gameField.score;
            bool Finished = gameField.finished;

            List<Card> tableDeck = gameField.tableDeck;
            List<Card> currentPlayer = gameField.playerA.Deck;
            List<Card> nextPlayer = gameField.playerB.Deck;

            string namePosition;

            Console.WriteLine("Turn: {0}, Score: {1}, Finished: {2}", Turn, Score, Finished);

            namePosition = "Current player: ";
            Console.CursorLeft = EMPTY_SPACE - namePosition.Length;
            Console.Write(namePosition);
            ShowDeck(currentPlayer);

            namePosition = "Next player: ";
            Console.CursorLeft = EMPTY_SPACE - namePosition.Length;
            Console.Write(namePosition);
            ShowDeck(nextPlayer);

            namePosition = "Table: ";
            Console.CursorLeft = EMPTY_SPACE - namePosition.Length;
            Console.Write(namePosition);
            ShowDeck(tableDeck);
        }
    }
}