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

        public GameEngine()
        {
            mGameField = new GameField();
            #region ExampleDeck
            mGameField.mainDeck = new List<Card>();

            mGameField.mainDeck = Input.CreateMainDeck(mGameField);
            #endregion
        }

        public void StartGame()
        {
            //Output.ShowDeck(mGameField.mainDeck);
           
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(mGameField.playerA); 
            }
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(mGameField.playerB);
            }

            Output.ShowStatus(mGameField);
        }

        private void GiveCard(Player player)
        {
            player.Deck.Add(mGameField.mainDeck.First<Card>());
            mGameField.mainDeck.Remove(mGameField.mainDeck.First<Card>());
        }

        //private void EndStep()
        //{
        //    //Передаем ход следующему игроку
        //    bool isCurrentPlayerA = mGameField.playerA.isCurrent;
            
        //    if (isCurrentPlayerA == true)
        //    {
        //        mGameField.playerB.isCurrent = true;
        //        mGameField.playerA.isCurrent = false;
        //    }
        //    else
        //    {
        //        mGameField.playerB.isCurrent = false;
        //        mGameField.playerA.isCurrent = true;
        //    }
        //    //Увеличиваем счетчик ходов
        //    mGameField.turn++;

        //}

        private void UseCard()
        {

        }
    }
}
