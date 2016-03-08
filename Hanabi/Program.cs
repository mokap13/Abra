using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameEngine gameEngine = new GameEngine();
                gameEngine.StartGame(); 
            }
        }
    }
}
