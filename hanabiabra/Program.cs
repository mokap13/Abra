using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanabiabra
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine gameEngine = null;
            string[] sourceData = File.ReadAllLines(@"C:\TEST\2-big.in");
            
            for (int i = 0; i < sourceData.Length; i++)
            {
                Command command = Input.ParseCommand(sourceData[i]);
                if (command.Name == "deck")
                {
                    gameEngine = new GameEngine();
                }
                gameEngine.StartGame(command);
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
