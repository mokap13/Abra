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
            
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd";
            psi.Arguments = @"/k test.bat hanabiabra.exe 2-big.in";
            psi.CreateNoWindow = false;
            Process.Start(psi);

            GameEngine gameEngine = null;
            string sourceData;

            while (true)
            {
                sourceData = Console.ReadLine();
                if (sourceData == null)
                    break;
                Command command = Input.ParseCommand(sourceData);

                if (command.Name == "Start")
                {
                    gameEngine = new GameEngine();
                    gameEngine.StartGame(command);
                }
                if (!gameEngine.Finished)
                {
                    gameEngine.MoveMake(command);
                }
                Output.ShowGameStatus(gameEngine.GetGameField);
            } 
            
        }
    }
}
