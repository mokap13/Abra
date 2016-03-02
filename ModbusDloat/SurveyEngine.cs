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

        public void PrepareSurvey()
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
                        SurveyModbus(device);
                        OutputOnScreen.Show(server);
                        Thread.Sleep(device.periodSurvey);
                        
                    }
                }
            }
        }

        public void StopSurvey()
        {

        }

        public void SurveyModbus(Device device)
        {
            const int MINIMUM_NUMBER_OF_POINTS = 2;

            int addressFirstTag = device.Tags.First<Tag>().Address;
            int addressLastTag = device.Tags.Last<Tag>().Address;
            int lengthAddresses = addressLastTag - addressFirstTag + MINIMUM_NUMBER_OF_POINTS;

            device.startAddress = (ushort)addressFirstTag;
            device.numberOfPoints = (ushort)lengthAddresses;

            ushort[] data = (ushort[])SendRequest(device);
            ushort[] temp = new ushort[2];
            
            foreach (var tag in device.Tags)
            {
                int indexTag = tag.Address - device.startAddress;
                temp[0] = data[indexTag];
                temp[1] = data[indexTag + 1];

            #region Перестановка байт(слов)
                switch (tag.shuffleBytes)
                {
                    case ShuffleBytes.NONE:
                        break;
                    case ShuffleBytes.HIGHER_WORD_AHEAD:
                        ShuffleWord(temp);
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
                        tag.valueUshort = temp[0];
                        break;
                    case DataType.FLOAT:
                        tag.valueFloat = ParseFloat(temp[0],temp[1]);
                        break;
                    case DataType.STRING:
                        break;
                    default:
                        break;
                }
            #endregion 
            }
        }

        public object SendRequest(Device device)
        {
            switch (device.functionModbus)
            {
                case FunctionModbus.COILS:
                    bool[] tempBool = device.Master.ReadCoils(
                       device.Address,
                       device.startAddress,
                       device.numberOfPoints);
                    return tempBool;
                case FunctionModbus.DISCRETE_INPUTS:
                    return null;
                case FunctionModbus.INPUT_REGISTERS:
                    return null;
                case FunctionModbus.HOLDING_REGISTERS:
                    ushort[] tempUshort = device.Master.ReadHoldingRegisters(
                        device.Address,
                        device.startAddress,
                        device.numberOfPoints);
                    return tempUshort;
                case FunctionModbus.SERVER_ONLY:
                    return null;
            }
            return null;
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
        /// <summary>
        /// Преобразуют 2 значения типа ushort, в одно типа float, в соответствии со стандартом IEEE 754
        /// </summary>
        /// <param name="dataUshort">Массив данных типа ushort</param>
        /// <returns>Массив данных типа float</returns>
        public float ParseFloat(ushort DataA, ushort DataB)
        {
            ushort[] dataUshort = new ushort[2];
            dataUshort[0] = DataA;
            dataUshort[1] = DataB;
            float[] dataFloat = new float[1];

            Buffer.BlockCopy(dataUshort, 0, dataFloat, 0, dataUshort.Length * 2);

            return dataFloat[0];
        }
        /// <summary>
        /// Преобразуют каждые 2 значения массива типа ushort, в одно типа float, в соответствии со стандартом IEEE 754
        /// </summary>
        /// <param name="dataUshort">Массив данных типа ushort</param>
        /// <returns>Массив данных типа float</returns>
        public float[] ParseFloat(ushort[] dataUshort)
        {
            float[] dataFloat = new float[dataUshort.Length / 2];
            Buffer.BlockCopy(dataUshort, 0, dataFloat, 0, dataUshort.Length * 2);
            return dataFloat;
        }
    }
}
