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
        
        private GameField mGameField;

        private Player mPlayerA;
        private Player mPlayerB;

        public GameEngine()
        {
            mPlayerA = new Player();
            mPlayerB = new Player();

            mGameField = new GameField();

            mGameField.currentPlayer = mPlayerA;
            mGameField.nextPlayer = mPlayerB;
            mGameField.mainDeck = new List<Card>();

            mGameField.mainDeck = Input.ReadMainDeck(mGameField);
        }

        public void StartGame()
        {
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(mPlayerA); 
            }
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(mPlayerB);
            }

            Output.ShowGameStatus(mGameField);

            Console.ReadLine();
            Input.ReadCommand(mGameField);
        }

        private void GiveCard(Player player)
        {
            player.Deck.Add(mGameField.mainDeck.First<Card>());
            mGameField.mainDeck.Remove(mGameField.mainDeck.First<Card>());
        }

        //private void EndStep()
        //{
        //    //Передаем ход следующему игроку
        //    bool isCurrentPlayerA = mGameField.mPlayerA.isCurrent;
            
        //    if (isCurrentPlayerA == true)
        //    {
        //        mGameField.mPlayerB.isCurrent = true;
        //        mGameField.mPlayerA.isCurrent = false;
        //    }
        //    else
        //    {
        //        mGameField.mPlayerB.isCurrent = false;
        //        mGameField.mPlayerA.isCurrent = true;
        //    }
        //    //Увеличиваем счетчик ходов
        //    mGameField.turn++;

        //}

        private void UseCard()
        {

        }
    }
}
