using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModbusSurvey
{


    class Program2
    {
        static void asd(string[] args)
        {
            Survey();
        }
        public static void Survey()
        {
            const int SIZE = 50;
            SerialPort port = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
            port.Open();
            ModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);
            RequestConfig config = new RequestConfig(1, 0, SIZE);//////////
            float[] values = new float[SIZE];
            ushort[] valuesInt = new ushort[SIZE];

            while (true)
            {
                Console.Clear();
                //values = ModbusFloatSurvey(port, master, config);
                //for (int i = 0; i < values.Length; i++)
                //{
                //    if (values[i] == 0)
                //        continue;
                //    Console.WriteLine(values[i].ToString("0.00"));
                //}

                valuesInt = ModbusIntSurvey(port, master, config);
                for (int i = 0; i < values.Length; i++)
                {
                    if (valuesInt[i] == 0)
                        continue;
                    Console.WriteLine(valuesInt[i].ToString("0.00"));
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// Обмен местами байт(Слов) для дальнейшей обработки int значений в float
        /// </summary>
        /// <param name="array">Передаваемый массив даннныйх</param>
        /// <returns></returns>
        ///
        public static ushort[] ModbusRegisterExchange(ushort[] array)
        {
            ushort temp;
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 != 0)
                {
                    temp = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = temp;
                }
            }
            return array;
        }
        /// <summary>
        /// Возвращает массив опрошенных регистров (float)
        /// </summary>
        /// <param name="port">Настроенный COM port</param>
        /// <param name="master">Настроенное ведущие устройство</param>
        /// <returns></returns>
        public static float[] ModbusFloatSurvey(SerialPort port, ModbusSerialMaster master, RequestConfig config)
        {
            //Настройки опроса
            byte slaveID = config._slaveAddress;
            ushort startAddress = config._startAddress;
            ushort lengthAddress = config._numberOfPoints;

            //Массивы для хранения данных опрашиваемых регистров
            ushort[] intData = new ushort[config._numberOfPoints];
            float[] floatData = new float[intData.Length / 2];

            //Опрашиваем регистры и сохраняяем их в массив
            intData = master.ReadHoldingRegisters(slaveID, startAddress, lengthAddress);

            //Меняем местами полученные байты (слова)
            intData = ModbusRegisterExchange(intData);

            //Обрабатываем полученные значения типа int в float по стандарту IEEE 754
            Buffer.BlockCopy(intData, 0, floatData, 0, intData.Length * 2);

            return floatData;
        }

        public static ushort[] ModbusIntSurvey(SerialPort port, ModbusSerialMaster master, RequestConfig config)
        {
            //Настройки опроса
            byte slaveID = config._slaveAddress;
            ushort startAddress = config._startAddress;
            ushort lengthAddress = config._numberOfPoints;

            //Массивы для хранения данных опрашиваемых регистров
            ushort[] intData = new ushort[config._numberOfPoints];
            float[] floatData = new float[intData.Length / 2];

            //Опрашиваем регистры и сохраняяем их в массив
            intData = master.ReadHoldingRegisters(slaveID, startAddress, lengthAddress);

            return intData;
        }
    }
}
