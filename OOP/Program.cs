using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            sum(b: 2, a: 44, c: 102);
        }
        public static void sum(int a = 1, int b = 2, int c = 3)
        {
            Console.WriteLine("a = {0} b = {1} c = {2}", a, b, c);
            Console.ReadKey();
        }
    }
}
