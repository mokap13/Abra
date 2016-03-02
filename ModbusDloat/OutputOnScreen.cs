using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class OutputOnScreen
    {
        /// <summary>
        /// Вывод на экран всех тегов
        /// </summary>
        /// <param name="server">Экземпляр сервера</param>
        public static void Show(Server server)
        {
            Console.Clear();
            foreach (var node in server.Nodes)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(node.Name);
                Console.WriteLine("------------------------------------");
                Console.ResetColor();

                foreach (var device in node.Devices)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(device.Name);
                    Console.WriteLine("------------------------------------");
                    Console.ResetColor();

                    foreach (var tag in device.Tags)
                    {
                        Console.Write(tag.Name + " --- ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = 32;
                        switch (tag.dataType)
                        {
                            case DataType.BOOL:
                                break;
                            case DataType.INT:
                                Console.WriteLine(tag.valueUshort.ToString());
                                break;
                            case DataType.FLOAT:
                                Console.WriteLine(tag.valueFloat.ToString("0.00"));
                                break;
                            case DataType.STRING:
                                break;
                        }
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
