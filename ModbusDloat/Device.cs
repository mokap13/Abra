using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class Device : IDescription
    {
        /// <summary>
        /// Название устройства
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Комментарии к устройству
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Список тегов
        /// </summary>
        public List<Tag> Tags;
        /// <summary>
        /// Ведущее(мастер) устройство
        /// </summary>
        public ModbusSerialMaster Master { get; set; }
        /// <summary>
        /// Тип протокола
        /// </summary>
        private DeviceType Type { get; set; }
        /// <summary>
        /// Тип обмена данными
        /// </summary>
        private ExchangeType exchangeType { get; set; }
        /// <summary>
        /// Адрес устройства
        /// </summary>
        public byte Address { get; set; }
        /// <summary>
        /// Допустимое время ответа устройства
        /// </summary>
        public TimeSpan timeResponse { get; set; }
        /// <summary>
        /// Кол-во игнорирований ошибок
        /// </summary>
        public ushort repeatAfterError { get; set; }
        /// <summary>
        /// Пауза после получения ошибки
        /// </summary>
        public TimeSpan timeRepeatAfterError { get; set; }
        /// <summary>
        /// Период опроса
        /// </summary>
        public TimeSpan periodSurvey { get; set; }
        /// <summary>
        /// Задержка послле получения ответа
        /// </summary>
        public TimeSpan delayAfterResponse { get; set; }
        /// <summary>
        /// Первый из запрашиваемых регистров
        /// </summary>
        public ushort startAddress { get; set; }
        /// <summary>
        /// Кол-во запрашиваемых регистров
        /// </summary>
        public ushort numberOfPoints { get; set; }
        /// <summary>
        /// Функция запроса протокола MODBUS
        /// </summary>
        public FunctionModbus functionModbus { get; set; }
        /// <summary>
        /// Тип доступа
        /// </summary>
        public AccessType accessType { get; set; }
        /// <summary>
        /// Конструктор с настройками по умолчанию
        /// </summary>
        public Device()
        {
            Tags = new List<Tag>();
            //Настройки по умолчанию
            Type = DeviceType.MODBUS;
            exchangeType = ExchangeType.RTU;
            Name = "Device";
            Address = 1;
            timeResponse = TimeSpan.FromMilliseconds(1000);
            repeatAfterError = 3;
            timeRepeatAfterError = TimeSpan.FromMilliseconds(10000);
            periodSurvey = TimeSpan.FromMilliseconds(1000);
            delayAfterResponse = TimeSpan.FromMilliseconds(4);
            startAddress = 0;
            numberOfPoints = 2;
            functionModbus = FunctionModbus.HOLDING_REGISTERS;
            accessType = AccessType.READ_ONLY;
        }

        public void CreateModbusMaster(Node node)
        {
            if (node is NodeCOM)
            {
                if (exchangeType == ExchangeType.RTU)
                    Master = ModbusSerialMaster.CreateRtu((node as NodeCOM).serialPort);
                else if (exchangeType == ExchangeType.ASCII)
                    Master = ModbusSerialMaster.CreateAscii((node as NodeCOM).serialPort); 
            }
            if (node is NodeTCP)
            {
                Master = ModbusSerialMaster.CreateRtu((node as NodeTCP).tcpClient);
            }
        }
    }
    /// <summary>
    /// Протокол устройства
    /// </summary>
    enum DeviceType
    {
        MODBUS, PROGRAM
    }
    enum ExchangeType
    {
        ASCII, RTU
    }
    /// <summary>
    /// Тип функции запроса
    /// </summary>
    enum FunctionModbus
    {
        COILS,
        DISCRETE_INPUTS,
        INPUT_REGISTERS,
        HOLDING_REGISTERS,
        SERVER_ONLY
    }
    /// <summary>
    /// Тип доступа к данным
    /// </summary>
    enum AccessType
    {
        READ_ONLY, WRITE_ONLY, READ_WRITE
    }
}
