using System;
using System.IO;

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
                if (command.Name == "Start")
                {
                    gameEngine = new GameEngine();
                    gameEngine.StartGame(command);
                }
                else if(!gameEngine.Finished)
                {
                    gameEngine.MoveMake(command);
                }
            }
            Console.ReadLine();
        }
    }
}
