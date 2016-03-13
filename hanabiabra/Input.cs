using System;
using System.Collections.Generic;
using System.Linq;

namespace hanabiabra
{
    static class Input
    {
        /// <summary>
        /// Получить колоду карт из строкового представления
        /// </summary>
        private static Deck GetDeckFromString(this string[] sourceData)
        {
            List<Card> inputDeck = new List<Card>();
            for (int i = 0; i < sourceData.Length; i++)
            {
                inputDeck.Add(new Card(sourceData[i]));
            }
            return new Deck(inputDeck);
        }
        /// <summary>
        /// Возвращает true если строковое представление имени
        /// карты соответствует формату
        /// </summary>
        private static bool IsValidCardName(string sourceData)
        {
            char[] validColors = { 'R', 'G', 'B', 'Y', 'W' };
            char[] validRanks = { '1', '2', '3', '4', '5' };
            if (validColors.Contains(sourceData[0])
                && validRanks.Contains(sourceData[1]))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если строковое представление индекса
        /// карты соответствует формату
        /// </summary>
        private static bool IsValidCardIndex(string sourceData)
        {
            char[] validNumbers = { '0', '1', '2', '3', '4' };
            if (validNumbers.Contains(sourceData[0]))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если строковое представление цвета
        /// карты соответствует формату
        /// </summary>
        private static bool IsValidCardColor(string sourceData)
        {
            string[] validColors = { "Red", "Green", "Blue", "White", "Yellow" };
            if (validColors.Contains(sourceData))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если строковое представление ранга
        /// карты соответствует формату
        /// </summary>
        private static bool IsValidCardRank(string sourceData)
        {
            string[] validRanks = { "1", "2", "3", "4", "5" };
            if (validRanks.Contains(sourceData))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если команда 'card' и её значение
        /// соответствует формату
        /// </summary>
        private static bool IsValidCommandCard(string[] sourceData)
        {
            if (sourceData.Count() == 1
                && IsValidCardIndex(sourceData.First()))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если команда 'cards' и её значение
        /// соответствует формату
        /// </summary>
        private static bool IsValidCommandCards(string[] sourceData)
        {
            if (sourceData.Length < 0
                && sourceData.Length > 4)
                return false;

            for (int i = 0; i < sourceData.Length; i++)
            {
                if (!IsValidCardIndex(sourceData[i]))
                    return false;
            }

            return true;
        }
        /// <summary>
        /// Возвращает true если команда 'color' и её значение
        /// соответствует формату
        /// </summary>
        private static bool IsValidCommandColor(string[] sourceData)
        {
            if (sourceData.Length == 1
                && IsValidCardColor(sourceData.First()))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает true если команда 'rank' и её значение
        /// соответствует формату
        /// </summary>
        private static bool IsValidCommandRank(string[] sourceData)
        {
            if (sourceData.Length == 1
                && IsValidCardRank(sourceData.First()))
                return true;

            return false;
        }
        /// <summary>
        /// Возвращает индекс подстроки с указанным значением в данном массиве
        /// или null если подстрока отсутствует
        /// </summary>
        private static int? GetIndex(this string[] sourceText, string text)
        {
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (sourceText[i] == text)
                {
                    return i;
                }
            }
            return null;
        }
        /// <summary>
        /// Возвращает массив индексов из данного массива строк
        /// </summary>
        private static int[] ToCardIndexes(this string[] sourceData)
        {
            const int NUMBER_OF_COMMANDS = 2;
            string[] commands = sourceData.Take(NUMBER_OF_COMMANDS).ToArray();
            sourceData = sourceData.Skip(NUMBER_OF_COMMANDS).ToArray();

            if (commands[0].Contains("for") && commands[1].Contains("cards"))
            {
                if (IsValidCommandCards(sourceData))
                {
                    return sourceData.ToIntArray();
                }
            }
            else
            {
                throw new Exception("Command don't contain " + commands);
            }
            return null;
        }
        /// <summary>
        /// Возвращает целочисенное значение первого элемента из данного массива строк
        /// </summary>
        private static int ToInt(this string[] sourceData)
        {
            int number = int.Parse(sourceData.First());
            return number;
        }
        /// <summary>
        /// Возвращает цвет карты из значения первого элемента данного массива строк
        /// </summary>
        private static CardColor ToCardColor(this string[] sourceData)
        {
            CardColor cardColor = (CardColor)Enum.Parse(typeof(CardColor), sourceData.First());
            return cardColor;
        }
        /// <summary>
        /// Возвращает массив целых чисел из данного масива строк
        /// </summary>
        private static int[] ToIntArray(this string[] sourceArray)
        {
            int[] distantArrray = new int[sourceArray.Length];
            for (int i = 0; i < sourceArray.Length; i++)
            {
                distantArrray[i] = int.Parse(sourceArray[i]);
            }
            return distantArrray;
        }
        /// <summary>
        /// Возвращает команду start
        /// </summary>
        /// <param name="mainCommandName">Имя родительской команды</param>
        /// <param name="sourceData">Исходные данные</param>
        private static Command Start(string mainCommandName, string[] sourceData)
        {
            string commandName = "deck";
            int? firstDataIndex = sourceData.GetIndex(commandName) + 1;

            if (!firstDataIndex.HasValue)
            {
                throw new Exception("Command is don't contain " + commandName);
            }

            int dataIndex = firstDataIndex.Value;

            sourceData = sourceData.Skip(dataIndex).ToArray();
            sourceData = sourceData.TakeWhile(check => IsValidCardName(check)).ToArray();

            return new Command(mainCommandName, sourceData.GetDeckFromString());
        }
        /// <summary>
        /// Возвращает команду Play или Drop
        /// </summary>
        /// <param name="mainCommandName">Имя родительской команды</param>
        /// <param name="sourceData">Исходные данные</param>
        private static Command PutCard(string mainCommandName, string[] sourceData)
        {
            string commandName = sourceData.First();
            sourceData = sourceData.Skip(1).ToArray();

            if (commandName == "card")
                if (IsValidCommandCard(sourceData))
                {
                    int cardIndex = ToInt(sourceData);
                    return new Command(mainCommandName, cardIndex);
                }
                else
                {
                    throw new Exception("Command after '" + commandName + "' is incorrect");
                }
            else
                throw new Exception("Command is don't contain " + commandName);
        }
        /// <summary>
        /// Возвращает команду Tell
        /// </summary>
        /// <param name="mainCommandName">Имя родительской команды</param>
        /// <param name="sourceData">Исходные данные</param>
        private static Command Tell(string[] sourceData)
        {
            string commandName = sourceData.First();
            sourceData = sourceData.Skip(1).ToArray();
            int[] cardNumbers;

            switch (commandName)
            {
                case "color":
                    CardColor cardColor = sourceData.ToCardColor();
                    sourceData = sourceData.Skip(1).ToArray();
                    cardNumbers = sourceData.ToCardIndexes();
                    return new Command(commandName, cardNumbers, cardColor);
                case "rank":
                    int cardRank = sourceData.ToInt();
                    sourceData = sourceData.Skip(1).ToArray();
                    cardNumbers = sourceData.ToCardIndexes();
                    return new Command(commandName, cardNumbers, cardRank);
                default:
                    throw new Exception("Command is don't contain " + commandName);
            }
        }
        /// <summary>
        /// Возвращает обработанный текст как экзампляр команды
        /// </summary>
        /// <param name="sourceData">Исходные данные</param>
        public static Command ParseCommand(string sourceData)
        {
            string[] splitSourceData = sourceData.Split(' ');
            string mainCommand = splitSourceData.First();
            splitSourceData = splitSourceData.Skip(1).ToArray();
            //Console.WriteLine(sourceData);
            switch (mainCommand)
            {
                case "Start":
                    return Start(mainCommand, splitSourceData);
                case "Play":
                    return PutCard(mainCommand, splitSourceData);
                case "Drop":
                    return PutCard(mainCommand, splitSourceData);
                case "Tell":
                    return Tell(splitSourceData);
                default:
                    throw new Exception("Main command is incorrect");
            }
        }
    }
}
