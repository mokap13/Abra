using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    static class Input
    {
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
            string sourceData = "Tell color Red for cards 1 2 3 4";//Console.ReadLine();

            //Набор возможных команд и значений
            string[] statementTypes = new string[]{
                "Start new game with deck",
                "Play card",
                "Drop card",
                "Tell color",
                "Red","Green","Blue","Yellow","White",
                "Tell rank",
                "1","2","3","4","5",
            };

            string[] statements = new string[30];
            int[] statementIndexes = new int[5];
            

            for (int i = 0; i < statements.Length; i++)
            {
                statements[i] = RemoveString(ref sourceData, statementTypes);
                if (statements[i] == null)
                    break;
            }

            for (int i = 2; true; i++)
            {
                if (statements[i] == null)
                    break;
                statementIndexes[i - 2] = int.Parse(statements[i]);
            }

            if (statements[0] == "Play card")
            {
                int choosedCard = int.Parse(statements[1]);
                player.PlayCard(gameField, choosedCard);
            }
            if (statements[0] == "Drop card")
            {
                int choosedCard = int.Parse(statements[1]);
                player.DropCard(gameField, choosedCard);
            }
            if (statements[0] == "Tell color")
            {
                CardColor cardColor = DetermineColor(statements[1]);
                player.TellColor(gameField, cardColor, statementIndexes);
            }
            if (statements[0] == "Tell rank")
            {
                CardRank cardRank = DetermineRank(statements[1]);
                player.TellRank(gameField, cardRank, statementIndexes);
            }
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
        /// <param name="colorName">Представление цвета карты(string)</param>
        /// <returns>Представление цвета карты(CardColor)</returns>
        public static CardColor DetermineColor(string colorName)
        {
            const int FIRST_CHAR_COLOR = 0;

            CardColor color = (CardColor)colorName[FIRST_CHAR_COLOR];
            return color;
        }
        /// <summary>
        /// Приводит тип string к CardRank
        /// </summary>
        /// <param name="rankName">Представление ценности карты(string)</param>
        /// <returns>Представление ценности карты(CardRank)</returns>
        public static CardRank DetermineRank(string rankName)
        {
            const int FIRST_CHAR_RANK = 0;

            CardRank rank = (CardRank)rankName[FIRST_CHAR_RANK];
            return rank;
        }

        
    }
}
