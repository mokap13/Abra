using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class Device:Node
    {
        /// <summary>
        /// Список тегов
        /// </summary>
        public List<Tag> Tags;
        /// <summary>
        /// Ведущее(мастер) устройство
        /// </summary>
        public static ModbusSerialMaster _deviceMaster { get; set; }
        /// <summary>
        /// Тип протокола
        /// </summary>
        public DeviceType _deviceType { get; set; }
        /// <summary>
        /// Название устройства
        /// </summary>
        public string _deviceName { get; set; }
        /// <summary>
        /// Комментарии к устройству
        /// </summary>
        private string _deviceComment { get; set; }
        /// <summary>
        /// Адрес устройства
        /// </summary>
        public  byte _deviceAddress { get; set; }
        /// <summary>
        /// Допустимое время ответа устройства
        /// </summary>
        public  TimeSpan _timeResponse { get; set; }
        /// <summary>
        /// Кол-во игнорирований ошибок
        /// </summary>
        public  ushort _repeatAfterError { get; set; }
        /// <summary>
        /// Пауза после получения ошибки
        /// </summary>
        public  TimeSpan _timeRepeatAfterError { get; set; }
        /// <summary>
        /// Период опроса
        /// </summary>
        public  TimeSpan _periodSurvey { get; set; }
        /// <summary>
        /// Задержка послле получения ответа
        /// </summary>
        public  TimeSpan _delayAfterResponse { get; set; }

        public Device()
        {
            Tags = new List<Tag>();
            //Настройки по умолчанию
            _deviceType = DeviceType.MODBUS;
            _deviceName = "Device";
            _deviceAddress = 1;
            _timeResponse = TimeSpan.FromMilliseconds(1000);
            _repeatAfterError = 3;
            _timeRepeatAfterError = TimeSpan.FromMilliseconds(10000);
            _periodSurvey = TimeSpan.FromMilliseconds(1000);
            _delayAfterResponse = TimeSpan.FromMilliseconds(4);
        }

        public void CreateModbusMaster()
        {
            if (_exchangeType == ExchangeType.RTU)
                _deviceMaster = ModbusSerialMaster.CreateRtu(_serialPort);
            else if (_exchangeType == ExchangeType.ASCII)
                _deviceMaster = ModbusSerialMaster.CreateAscii(_serialPort);
        }
    }
    /// <summary>
    /// Протокол устройства
    /// </summary>
    enum DeviceType
    {
        MODBUS,PROGRAM
    }
}
