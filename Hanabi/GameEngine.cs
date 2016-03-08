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
        private Command mCommand;
        private GameField mGameField;
        private Player mPlayerA;
        private Player mPlayerB;
        private Deck mMainDeck;
        private Deck mTableDeck;

        public GameEngine()
        {
            mGameField = new GameField();

            mPlayerA = new Player();
            mPlayerB = new Player();
            mGameField.currentPlayer = mPlayerA;
            mGameField.nextPlayer = mPlayerB;

            mMainDeck = mGameField.mainDeck;
            mTableDeck = mGameField.tableDeck;

            mGameField.UpdateDecksName();
        }

        public void StartGame()
        {
            mGameField.mainDeck = Input.ReadMainDeck(mGameField);

            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(mPlayerA,mMainDeck); 
            }
            for (int i = 0; i < START_SIZE_PLAYER_DECK; i++)
            {
                GiveCard(mPlayerB,mMainDeck);
            }

            Output.ShowGameStatus(mGameField);

            mCommand = Input.ReadCommand();

            CheckCommand(mCommand, mGameField);
            Console.WriteLine();
        }

        private void GiveCard(Player player,Deck sourceDeck)
        {
            Card pullCard = mGameField.mainDeck.PullTopCard(sourceDeck);
            player.Deck.Cards.Add(pullCard);
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

        private bool? CheckCommand(Command command,GameField gamefield)
        {
            switch (command.CommandName)
            {
                case CommandName.Playcard:
                    Card choosedCard = gamefield.currentPlayer.Deck.Cards[command.ChoosedCards[0] - 1];
                    if ((int)choosedCard.Rank <= gamefield.tableDeck.GetMaxRank(choosedCard.Color));
                        return false;
                    break;
                case CommandName.Dropcard:
                    break;
                case CommandName.Tellcolor:
                    break;
                case CommandName.Tellrank:
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
