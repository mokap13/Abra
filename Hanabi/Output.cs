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
                card.Show();
                Console.Write(" ");
            }
            Console.Write("\n");
        }
        public static void ShowPlayer(Player player)
        {
            Console.WriteLine("***" + player.mName + "***");
        }
        public static void ShowStatus(GameField gameField)
        {
            const int EMPTY_SPACE = 15;
            int Turn = gameField.Turn;
            int Score = gameField.Score;
            bool Finished = gameField.Finished;

            List<Card> tableDeck = gameField.tableDeck;
            List<Card> currentPlayer = gameField.player_A.mDeck;
            List<Card> nextPlayer = gameField.player_B.mDeck;

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