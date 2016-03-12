﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hanabi
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sourceData = File.ReadAllLines(@"C:\TEST\2-big.in");

            for (int j = 0; j < sourceData.Length; )
            {
                GameEngine gameEngine = new GameEngine();
                gameEngine.StartGame(sourceData);
            }
            Console.ReadLine();
        }
    }
}
