using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class GameEngine
    {
        private List<Card> mainDeck;
        private Player player_A;
        private Player player_B;S
        private GameField gameField;

        public GameEngine()
        {
            #region ExampleDeck
            mainDeck = new List<Card>();

            mainDeck.Add(new Card(1, ConsoleColor.Red));
            mainDeck.Add(new Card(2, ConsoleColor.Green));
            mainDeck.Add(new Card(3, ConsoleColor.Blue));
            mainDeck.Add(new Card(4, ConsoleColor.White));
            mainDeck.Add(new Card(5, ConsoleColor.Yellow));
            mainDeck.Add(new Card(1, ConsoleColor.Red));
            mainDeck.Add(new Card(1, ConsoleColor.Red));
            mainDeck.Add(new Card(1, ConsoleColor.Blue));
            mainDeck.Add(new Card(2, ConsoleColor.Blue));
            mainDeck.Add(new Card(1, ConsoleColor.White));
            mainDeck.Add(new Card(2, ConsoleColor.White));
            mainDeck.Add(new Card(1, ConsoleColor.White));
            #endregion

            gameField = new GameField(mainDeck);

            player_A = new Player(mainDeck.GetRange(0,5));
            player_B = new Player(mainDeck.GetRange(5,5));
        }

        public void StartGame()
        {
            Output.ShowDeck(mainDeck);
            Output.ShowDeck(player_A.mDeck);
            Output.ShowDeck(player_B.mDeck);
        }
    }
}
