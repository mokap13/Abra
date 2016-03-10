using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace input
{
    static class newInput
    {
        public static void ReadCommand()
        {
            string sourceData = "Start new game with deck R1 G2 B3 W4 Y5 R1 R1 B1 B2 W1 W2 W1";
            string[] splitSourceData = sourceData.Split(' ');
            string mainCommand = splitSourceData.Take(1).ToArray()[0];
            splitSourceData = splitSourceData.Skip(1).ToArray();

            switch (mainCommand)
            {
                case "Start":
                    Start(splitSourceData);
                    break;
                case "Play":
                    break;
                case "Drop":
                    break;
                case "Tell":
                    break;
                default:
                    Console.WriteLine("Command is don't contain Main Command");
                    Console.ReadLine();
                    break;
            }

            Console.WriteLine();
            Console.ReadLine();
        }

        private static List<newCard> GetDeckFromString(this string[] sourceData)
        {
            List<newCard> deck = new List<newCard>();
            for (int i = 0; i < sourceData.Length; i++)
            {
                deck.Add(new newCard(sourceData[i]));
            }
            return deck;
        }

        private static bool CheckStringCard(string sourceData)
        {
            char[] validColors = { 'R', 'G', 'B', 'Y', 'W' };
            char[] validRanks = { '1', '2', '3', '4', '5' };
            if (!validColors.Contains(sourceData[0])
                || !validRanks.Contains(sourceData[1]))
            {
                return false;
            }
            return true;
        }

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

        private static void Start(string[] sourceData)
        {
            int?firstDataIndex = sourceData.GetIndex("deck") + 1;
            if (!firstDataIndex.HasValue)
            {
                throw new Exception("Command is don't contain 'deck'");
            }
            int dataIndex = firstDataIndex.Value;

            sourceData = sourceData.Skip(dataIndex).ToArray();

            for (int i = dataIndex; i < sourceData.Length; i++)
            {
                sourceData = sourceData.TakeWhile(check => CheckStringCard(check)).ToArray();
            }
            List<newCard> deck = sourceData.GetDeckFromString();
            Console.WriteLine();
        }
    }
}
