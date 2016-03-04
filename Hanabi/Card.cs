using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanabi
{
    class Card
    {
        //Имя карты
        private string name;
        //Цвет карты
        public ConsoleColor mColor;
        //Ценность карты
        public int mValue;

        public Card(int value,ConsoleColor color)
        {
            mColor = color;
            mValue = value;
            name = (mColor.ToString()[0] + mValue.ToString());
        }
        //Представление карты на экране
        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = mColor;
            Console.Write(name);
            Console.ResetColor();
        }
    }
}
