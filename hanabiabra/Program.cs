using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace hanabiabra
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine gameEngine = null;
            //string[] sourceData = File.ReadAllLines(@"C:\TEST\1-1.in");
            string[] crudeSourceData = new string[80000];
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd";
            psi.Arguments = @"/k test.bat hanabiabra.exe 2-big.in";
            Process.Start(psi);
            int indexLastElement = 0;
            for (int i = 0; i < crudeSourceData.Length; i++)
            {
                crudeSourceData[i] = Console.ReadLine();
                if (crudeSourceData[i] == null)
                {
                    indexLastElement = i;
                    break;
                }
                else
                    continue;
            }
            string[] sourceData = new string[indexLastElement];

            for (int i = 0; i < sourceData.Length; i++)
            {
                sourceData[i] = crudeSourceData[i];
            }

            for (int i = 0; i < indexLastElement; i++)
            {
                Command command = Input.ParseCommand(crudeSourceData[i]);
                if (command.Name == "Start")
                {
                    gameEngine = new GameEngine();
                    gameEngine.StartGame(command);
                }
                else if (!gameEngine.Finished)
                {
                    gameEngine.MoveMake(command);
                }
                Output.ShowGameStatus(gameEngine.GetGameField);
            }
            //Thread.Sleep(2000);
        }
    }
}
