using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    static class Input
    {
        private static string[] cardColors = new string[] { "Red", "Green", "Blue", "Yellow", "White" };
        private static string[] cardRanks = new string[] { "1", "2", "3", "4", "5" };
        private static string[] commands = {"Start", "new", "game", "with", "deck","Dropcard","Playcard","Tellcolor","Tellrank"};

        public static Deck ReadMainDeck(GameField gameField)
        {
            Deck mainDeck = gameField.mainDeck;

            const int CARD_COLOR_SOCKET = 0;
            const int CARD_VALUE_SOCKET = 1;

            string inputData = null;
            string[] inputDataArray = null;

            while (true)
            {
                Console.Write(">");
                //sourceData = Console.ReadLine();
                inputData = ("Start new game with deck " +
                "Y4 W3 B1 Y1 G4 G1 W1 Y3 R1 G3 Y3 R4 W1 " +
                "Y2 B5 R1 W1 R5 W3 B1 Y5 G5 R4 B3 W2 Y2 W4 " +
                "Y4 B4 G2 R3 B1 Y1 W4 B4 G2 B2 G3 B3 W2 G1 " +
                "W5 G1 B2 R2 Y1 R3 R1 R2 G4");
                Console.WriteLine(inputData);

                if (inputData.Contains("Start new game with deck "))
                {
                    //Преобразуем входную строку в массив, используя 'Space' как разделитель
                    inputDataArray = inputData.Split(' ');

                    foreach (string data in inputDataArray)
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

        public static Command ReadCommand()
        {
            Console.Write(">");
            Command command;

            string sourceData = "Tell color Red for cards 1 2 3 4";//Console.ReadLine();
            string[] splitData = sourceData.Split(' ');

            int choosedCard = 0;
            int[] choosedCards = new int[5];
            CardColor? cardColor;
            CardRank? cardRank;
            CommandName? commandName;
            
            commandName = DetermineCommandName(splitData[0]+splitData[1]);
            if (commandName == null)
            {
                Console.WriteLine("command is not existing");
                Console.ReadLine();
                return null;
            }

            if (splitData[0] == "Play" || splitData[0] == "Drop")
            {
                choosedCard = int.Parse(splitData[2]);
                command = new Command(commandName,choosedCard);
                return command;
            }

            cardColor = DetermineColor(splitData[2]);
            cardRank = DetermineRank(splitData[2]);

            int indexStartChoosedCards = GetIndex(splitData, "cards");

            if (splitData[3] == "for" && splitData[4] == "cards")
                for (int j = 0; j < splitData.Length - 5; j++)
                {
                    choosedCards[j] = int.Parse(splitData[j + 5]);
                }

            command = new Command(commandName,choosedCard,choosedCards,cardColor,cardRank);
            return command;
        }

        /// <summary>
        /// Извлекает первую найденую подстроку в тексте, значение котрой соответствует одной из строк массива statementsArray
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="statementsArray">Массив искомых подстрок</param>
        /// <returns>Первая найденая строка, значение которой 
        /// соответсвует значению одной из строк массива statementsArray</returns>
        private static string RemoveString(ref string text, string[] statementsArray)
        {
            for (int i = 0; i < statementsArray.Length; i++)
            {
                if (text.Contains(statementsArray[i]))
                {
                    string statement = statementsArray[i];
                    int statementEndIndex = text.IndexOf(statement) + statement.Length;
                    text = text.Substring(statementEndIndex);

                    return statement;
                }
            }
            return null;
        }
        /// <summary>
        /// Приводит тип string к CardColor
        /// </summary>
        /// <param name="sourceName">Представление цвета карты(string)</param>
        /// <returns>Представление цвета карты(CardColor)</returns>
        private static CardColor? DetermineColor(string sourceName)
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
        private static CardRank? DetermineRank(string sourceName)
        {
            if (cardRanks.Contains(sourceName) == false)
                return null;
            const int FIRST_CHAR_RANK = 0;
            CardRank rank = (CardRank)sourceName[FIRST_CHAR_RANK];
            return rank;
        }

        private static int GetIndex(string[] sourceText, string text)
        {
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (sourceText[i] == text)
                    return i;
            }
            return -1;
        }

        private static CommandName? DetermineCommandName(string sourceName)
        {
            if (commands.Contains(sourceName) == false)
                return null;
            
            CommandName commandName = (CommandName)Enum.Parse(typeof(CommandName), sourceName);
            return commandName;
        }
    }
}
