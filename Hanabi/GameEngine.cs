using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class GameEngine
    {
        const int START_SIZE_PLAYER_DECK = 5;
            
        private GameField gameField;
        //private List<Player> mPlayers;

        public GameEngine()
        {
            gameField = new GameField();
            #region ExampleDeck
            gameField.mainDeck = new List<Card>();

            gameField.mainDeck.Add(new Card(1, ConsoleColor.Red));
            gameField.mainDeck.Add(new Card(2, ConsoleColor.Green));
            gameField.mainDeck.Add(new Card(3, ConsoleColor.Blue));
            gameField.mainDeck.Add(new Card(4, ConsoleColor.White));
            gameField.mainDeck.Add(new Card(5, ConsoleColor.Yellow));
            gameField.mainDeck.Add(new Card(1, ConsoleColor.Red));
            gameField.mainDeck.Add(new Card(1, ConsoleColor.Red));
            gameField.mainDeck.Add(new Card(1, ConsoleColor.Blue));
            gameField.mainDeck.Add(new Card(2, ConsoleColor.Blue));
            gameField.mainDeck.Add(new Card(1, ConsoleColor.White));
            gameField.mainDeck.Add(new Card(2, ConsoleColor.White));
            gameField.mainDeck.Add(new Card(1, ConsoleColor.White));

            
            #endregion
        }

        public void StartGame()
        {
            //Выводим колоду на экран
            Output.ShowDeck(gameField.mainDeck);
            //Инициализируем двух игроков
            gameField.player_A = new Player();
            gameField.player_B = new Player();
            gameField.tableDeck = new List<Card>();

            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(gameField.player_A); 
            }
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(gameField.player_B);
            }

            Output.ShowStatus(gameField);
        }

        private void GiveCard(Player player)
        {
            player.mDeck.Add(gameField.mainDeck.First<Card>());
            gameField.mainDeck.Remove(gameField.mainDeck.First<Card>());
        }
    }
}
