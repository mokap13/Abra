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

        public static List<Card> ReadMainDeck(GameField gameField)
        {
            List<Card> mainDeck = gameField.mainDeck;

            const int CARD_COLOR_SOCKET = 0;
            const int CARD_VALUE_SOCKET = 1;

            string inputCommand = null;
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
                        Card card = new Card(data[CARD_COLOR_SOCKET], data[CARD_VALUE_SOCKET]);
                        mainDeck.Add(card);
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

        public static void ReadCommand(GameField gameField)
        {
            Console.Write(">");
            Player player = gameField.currentPlayer;
            Command command;

            string sourceData = "Tell color Red for cards 1 2 3 4";//Console.ReadLine();
            string[] splitData = sourceData.Split(' ');

            string commandName;
            int choosedCard;
            int[] choosedCards = new int[5];
            CardColor? cardColor;
            CardRank? cardRank;

            commandName = splitData[0];
            int.TryParse(splitData[2], out choosedCard);
            cardColor = DetermineColor(splitData[2]);
            cardRank = DetermineRank(splitData[2]);

            if (splitData[3] + splitData[4] == "forcards")
                for (int j = 0; j < splitData.Length - 5; j++)
                {
                    choosedCards[j] = int.Parse(splitData[j + 5]);
                }

            command = new Command(commandName,choosedCard,choosedCards,cardColor,cardRank);
            gameField.currentCommand = command;
            Console.WriteLine();
        }

        /// <summary>
        /// Извлекает первую найденую подстроку в тексте, значение котрой соответствует одной из строк массива statementsArray
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="statementsArray">Массив искомых подстрок</param>
        /// <returns>Первая найденая строка, значение которой 
        /// соответсвует значению одной из строк массива statementsArray</returns>
        public static string RemoveString(ref string text, string[] statementsArray)
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
        /// <param name="rankName">Представление цвета карты(string)</param>
        /// <returns>Представление цвета карты(CardColor)</returns>
        public static CardColor? DetermineColor(string rankName)
        {
            if (cardColors.Contains(rankName) == false)
                return null;
            const int FIRST_CHAR_COLOR = 0;
            CardColor color = (CardColor)rankName[FIRST_CHAR_COLOR];
            return color;
        }
        /// <summary>
        /// Приводит тип string к CardRank
        /// </summary>
        /// <param name="rankName">Представление ценности карты(string)</param>
        /// <returns>Представление ценности карты(CardRank)</returns>
        public static CardRank? DetermineRank(string rankName)
        {
            if (cardRanks.Contains(rankName) == false)
                return null;
            const int FIRST_CHAR_RANK = 0;
            CardRank rank = (CardRank)rankName[FIRST_CHAR_RANK];
            return rank;
        }

        public static int GetIndex(string[] sourceText, string text)
        {
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (sourceText[i] == text)
                    return i;
            }
            return -1;
        }
    }
}
