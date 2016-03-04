using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Program
    {
        //R1 G2 B3 W4 Y5 R1 R1 B1 B2 W1 W2 W1

        static void Main(string[] args)
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.StartGame();
            Console.ReadKey();
        }
    }
}
