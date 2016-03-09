using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hanabi
{
    static class Input
    {
        /// <summary>
        /// Разделитель
        /// </summary>
        private static char DELIMITER = ' ';
        /// <summary>
        /// Множество возможных цветов в строковом представлении
        /// </summary>
        private static string[] cardColors = new string[] { "Red", "Green", "Blue", "Yellow", "White" };
        /// <summary>
        /// Множество возможных рангов в строковом представлении 
        /// </summary>
        private static string[] cardRanks = new string[] { "1", "2", "3", "4", "5" };
        /// <summary>
        /// Множество возможных команд в строковом представлении
        /// </summary>
        private static string[] commands = { "Start", "new", "game", "with", "deck", "Dropcard", "Playcard", "Tellcolor", "Tellrank" };

        private static CommandName? DefineCommandName(string sourceName)
        {
            if (commands.Contains(sourceName) == false)
                return null;

            CommandName commandName = (CommandName)Enum.Parse(typeof(CommandName), sourceName);
            return commandName;
        }
        /// <summary>
        /// Приводит тип string к CardColor
        /// </summary>
        /// <param name="sourceName">Представление цвета карты(string)</param>
        /// <returns>Представление цвета карты(CardColor)</returns>
        private static CardColor? DefineColor(string sourceName)
        {
            if (cardColors.Contains(sourceName) == false)
                return null;
            const int FIRST_CHAR_COLOR = 0;
            CardColor color = (CardColor)sourceName[FIRST_CHAR_COLOR];
            return color;
        }
        /// <summary>
        /// Приводит тип string к CardRank
        /// </summary>
        /// <param name="sourceName">Представление ценности карты(string)</param>
        /// <returns>Представление ценности карты(CardRank)</returns>
        private static int? DefineRank(string sourceName)
        {
            if (cardRanks.Contains(sourceName) == false)
                return null;
            const int FIRST_CHAR_RANK = 0;
            int rank = (int)Char.GetNumericValue(sourceName[FIRST_CHAR_RANK]);
            return rank;
        }

        private static int GetIndex(string[] sourceText, string text)
        {
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (sourceText[i] == text)
                    return i;
            }
            return sourceText.Length;
        }

        public static Deck ReadMainDeck(GameField gameField, string sourceData)
        {
            //Console.WriteLine(sourceData);
            Deck mainDeck = gameField.mainDeck;

            const int CARD_COLOR_SOCKET = 0;
            const int CARD_VALUE_SOCKET = 1;

            //sourceData = null;
            string[] sourceDataArray = null;

            while (true)
            {
                //Console.Write(">");
                //sourceData = Console.ReadLine();
               // sourceData = Console.ReadLine();
                //Console.WriteLine(sourceData);

                if (sourceData.Contains("Start new game with deck "))
                {
                    //Преобразуем входную строку в массив, используя 'Space' как разделитель
                    sourceDataArray = sourceData.Split(DELIMITER);

                    foreach (string data in sourceDataArray)
                    {
                        if (commands.Contains(data))
                            continue;

                        Card card = new Card(data[CARD_COLOR_SOCKET], data[CARD_VALUE_SOCKET]);
                        mainDeck.Cards.Add(card);
                    }
                    return mainDeck;
                }
                else
                {
                    Console.WriteLine("Команда не корректна");
                    Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Читает из входного потока введенную пользователем команду
        /// </summary>
        /// <returns>Команда</returns>
        public static Command ReadCommand(string sourceData)
        {
            //Console.WriteLine(">" + sourceData);
            const int INDEX_CHOOSED_VALUE = 2;
            const int INDEX_CHOOSED_CARDS = 5;
            //Ввод данных из консоли
            //Console.Write(">");
            //string sourceData = Console.ReadLine();
            string[] splitData = sourceData.Split(DELIMITER);

            #region Параметры команды
            Command command;
            int[] choosedCards = null;
            CardColor? cardColor = null;
            int? cardRank = null;
            CommandName? commandName = null;
            int numbersChoosedCards; 
            #endregion

            commandName = DefineCommandName(splitData[0] + splitData[1]);

            if (commandName == null)
            {
                Console.WriteLine("Command incorrect");
                Console.ReadLine();
                return null;
            }

            #region Play and Drop
            if (commandName == CommandName.Playcard || commandName == CommandName.Dropcard)
            {
                numbersChoosedCards = splitData.Length - INDEX_CHOOSED_VALUE;
                choosedCards = new int[numbersChoosedCards];
                choosedCards[0] = int.Parse(splitData[INDEX_CHOOSED_VALUE]);
            } 
            #endregion
            #region Tellcolor and Tellrank
            if (commandName == CommandName.Tellcolor || commandName == CommandName.Tellrank)
            {
                cardColor = DefineColor(splitData[INDEX_CHOOSED_VALUE]);
                cardRank = DefineRank(splitData[INDEX_CHOOSED_VALUE]);

                numbersChoosedCards = splitData.Length - INDEX_CHOOSED_CARDS;
                choosedCards = new int[numbersChoosedCards];

                for (int i = INDEX_CHOOSED_CARDS; i < splitData.Length; i++)
                {
                    choosedCards[i - INDEX_CHOOSED_CARDS] = int.Parse(splitData[i]);
                }
            } 
            #endregion

            command = new Command(commandName, choosedCards, cardColor, cardRank);
            return command;
        }
    }
}
