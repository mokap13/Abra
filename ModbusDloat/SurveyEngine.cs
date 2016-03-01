using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using System.IO.Ports;
using System.Threading;

namespace ModbusSurvey
{
    class SurveyEngine
    {
        Server server;

        public SurveyEngine(Server _server)
        {
            server = _server;
        }

        public void StartSurvey()
        {

            while (true)
            {
                foreach (var node in server.Nodes)
                {
                    //Создаем порт и открываем его, если он еще не создан
                    if (node.serialPort == null || node.serialPort.IsOpen == false)
                    {
                        node.CreateSerialPort();
                        node.serialPort.Open(); 
                    }
                    foreach (var device in node.Devices)
                    {
                        if (device.Master == null) 
                        {
                            device.CreateModbusMaster(node); 
                        }
                        foreach (var tag in device.Tags)
                        {
                            SurveyModbus(tag, device);
                            OutputOnScreen(tag);
                        }
                    }
                }
            }
        }

        public void StopSurvey()
        {

        }

        public object QueryModbus(Tag tag, Device device)
        {
            switch (tag.functionModbus)
            {
                case FunctionModbus.COILS:
                    bool[] tempBool = device.Master.ReadCoils(
                       device.Address,
                       tag.startAddress,
                       tag.numberOfPoints);
                    return tempBool;
                case FunctionModbus.DISCRETE_INPUTS:
                    Console.WriteLine("ERROR");
                    Console.ReadKey();
                    return null;
                case FunctionModbus.INPUT_REGISTERS:
                    Console.WriteLine("ERROR");
                    Console.ReadKey();
                    return null;
                case FunctionModbus.HOLDING_REGISTERS:
                    ushort[] tempUshort = device.Master.ReadHoldingRegisters(
                        device.Address,
                        tag.startAddress,
                        tag.numberOfPoints);
                    return tempUshort;
                case FunctionModbus.SERVER_ONLY:
                    Console.ReadKey();
                    Console.WriteLine("ERROR");
                    return null;
            }
            Console.WriteLine("ERROR");
            Console.ReadKey();
            return null;
        }

        public void SurveyModbus(Tag tag, Device device)
        {
            ushort[] data = (ushort[])QueryModbus(tag, device);

            #region Перестановка байт(слов)
            switch (tag.shuffleBytes)
            {
                case ShuffleBytes.NONE:
                    break;
                case ShuffleBytes.HIGHER_WORD_AHEAD:
                    ShuffleWord(data);
                    break;
                case ShuffleBytes.HIGHER_BYTE_AHEAD:
                    break;
            }
            #endregion

            #region Обработка типа данных
            switch (tag.dataType)
            {
                case DataType.BOOL:
                    break;
                case DataType.INT:
                    tag.valueUshort = data;
                    break;
                case DataType.FLOAT:
                    //Получем float Значение путем преобразования по стандурту IEEE 754
                    float[] dataFloat = DataFloat(data);
                    //Записываем значение в тег
                    tag.valueFloat = (float[])dataFloat;
                    break;
                case DataType.STRING:
                    break;
                default:
                    break;
            }
            #endregion
        }

        public void OutputOnScreen(Tag tag)
        {
            switch (tag.dataType)
            {
                case DataType.BOOL:
                    break;
                case DataType.INT:
                    for (int i = 0; i < tag.valueUshort.Length; i++)
                    {
                        if (tag.valueUshort[i] == 0)
                            continue;
                        Console.Write(tag.ToString() + " --- ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = 32;
                        Console.WriteLine(tag.valueUshort[i].ToString());
                        Console.ResetColor();
                    }
                    break;
                case DataType.FLOAT:
                    for (int i = 0; i < tag.valueFloat.Length; i++)
                    {
                        if (tag.valueFloat[i] == 0)
                            continue;
                        Console.Write(tag.ToString() + " --- ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = 32;
                        Console.WriteLine(tag.valueFloat[i].ToString("0.00"));
                        Console.ResetColor();
                    }
                    break;
                case DataType.STRING:
                    break;
            }
        }


        /// <summary>
        /// Преобразуют каждые 2 значения массива типа ushort, в одно типа float, в соответствии со стандартом IEEE 754
        /// </summary>
        /// <param name="dataUshort">Массив данных типа ushort</param>
        /// <returns>Массив данных типа float</returns>
        public float[] DataFloat(ushort[] dataUshort)
        {
            float[] dataFloat = new float[dataUshort.Length / 2];
            Buffer.BlockCopy(dataUshort, 0, dataFloat, 0, dataUshort.Length * 2);
            return dataFloat;
        }
        /// <summary>
        /// Меняет местами четные и нечетные ячейки массива
        /// </summary>
        /// <typeparam name="T">Произвольный массив данных</typeparam>
        /// <param name="array">Обработанный массив данных</param>
        public void ShuffleWord<T>(T[] array)
        {
            T temp;
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 != 0)
                {
                    temp = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = temp;
                }
            }
        }
    }
}
