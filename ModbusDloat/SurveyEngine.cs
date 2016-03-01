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
        Server _server;

        public SurveyEngine(Server server)
        {
            _server = server;
        }

        public void StartSurvey()
        {

            while (true)
            {
                foreach (var node in _server.Nodes)
                {
                    foreach (var device in node.Devices)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(device._deviceName);
                        Console.ResetColor();
                        foreach (var tag in device.Tags)
                        {
                            SurveyModbus(tag,device);
                            OutputOnScreen(tag);
                        }
                        Console.WriteLine("----------------------------------------");
                        Thread.Sleep(device._periodSurvey);
                        Console.Clear();
                    }
                }
            }
        }

        public void StopSurvey()
        {

        }

        public object QueryModbus(Tag tag,Device device)
        {
            switch (tag._functionModbus)
            {
                case FunctionModbus.COILS:
                    bool[] tempBool = device._deviceMaster.ReadCoils(
                       device._deviceAddress,
                       tag._startAddress,
                       tag._numberOfPoints);
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
                    ushort[] tempUshort = device._deviceMaster.ReadHoldingRegisters(
                        device._deviceAddress,
                        tag._startAddress,
                        tag._numberOfPoints);
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

        public float[] DataFloat(ushort[] dataUshort)
        {
            float[] dataFloat = new float[dataUshort.Length / 2];
            Buffer.BlockCopy(dataUshort, 0, dataFloat, 0, dataUshort.Length * 2);
            return dataFloat;
        }

        public void SurveyModbus(Tag tag,Device device)
        {
            ushort[] data = (ushort[])QueryModbus(tag,device);

            #region Перестановка байт(слов)
            switch (tag._shuffleBytes)
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
            switch (tag._dataType)
            {
                case DataType.BOOL:
                    break;
                case DataType.INT:
                    tag._valueUshort = data;
                    break;
                case DataType.FLOAT:
                    //Получем float Значение путем преобразования по стандурту IEEE 754
                    float[] dataFloat = DataFloat(data);
                    //Записываем значение в тег
                    tag._valueFloat = (float[])dataFloat;
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
            switch (tag._dataType)
            {
                case DataType.BOOL:
                    break;
                case DataType.INT:
                    for (int i = 0; i < tag._valueUshort.Length; i++)
                    {
                        if (tag._valueUshort[i] == 0)
                            continue;
                        Console.Write(tag._tagName + " --- ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = 32;
                        Console.WriteLine(tag._valueUshort[i].ToString());
                        Console.ResetColor();
                    }
                    break;
                case DataType.FLOAT:
                    for (int i = 0; i < tag._valueFloat.Length; i++)
                    {
                        if (tag._valueFloat[i] == 0)
                            continue;
                        Console.Write(tag._tagName + " --- ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.CursorLeft = 32;
                        Console.WriteLine(tag._valueFloat[i].ToString("0.00"));
                        Console.ResetColor();
                    }
                    break;
                case DataType.STRING:
                    break;
            }
        }

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
