using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class Node:Server
    {
        /// <summary>
        /// Список устройств узла
        /// </summary>
        public List<Device> Devices;
        /// <summary>
        /// Последовательный порт
        /// </summary>
        public static SerialPort _serialPort { get; set; }
        /// <summary>
        /// Имя узла
        /// </summary>
        public string _nodeName { get; set; }
        /// <summary>
        /// Тип узла
        /// </summary>
        public static NodeType _nodeType { get; set; }
        /// <summary>
        /// Комментарий узла
        /// </summary>
        public static string _nodeComment { get; set; }

        /// <summary>
        /// Имя порта
        /// </summary>
        public string _portName { get; set; }
        /// <summary>
        /// Скорость порта
        /// </summary>
        public ushort _baudRate { get; set; }
        /// <summary>
        /// Четность порта
        /// </summary>
        public Parity _parity { get; set; }
        /// <summary>
        /// Длина адреса порта
        /// </summary>
        public ushort _dataBits { get; set; }
        /// <summary>
        /// Стоп-биты
        /// </summary>
        public StopBits _stopBits { get; set; }
        /// <summary>
        /// Тип обмена данными
        /// </summary>
        public ExchangeType _exchangeType { get; set; }
        
        /// <summary>
        /// IP адрес 
        /// </summary>
        public string _ipAddress;
        /// <summary>
        /// Номер порта
        /// </summary>
        public ushort _ipPort;

        /// <summary>
        /// Конструктор с настройками "по умолчанию"
        /// </summary>
        public Node()
        {
            Devices = new List<Device>();
            //Характеристики узла
            _nodeName = "Узел";
            _nodeType = NodeType.COM;

            //Характеристики COM порта
            _portName = "COM4";
            _baudRate = 9600;
            _parity = Parity.None;
            _dataBits = 8;
            _stopBits = StopBits.One;
            _exchangeType = ExchangeType.RTU;
        
            //Харакатиристики TCP/IP
            _ipAddress = "127.0.0.0";
            _ipPort = 502;
        }
        public Node(string name)
        {
            Devices = new List<Device>();
            //Характеристики узла
            _nodeName = name;
            _nodeType = NodeType.COM;

            //Характеристики COM порта
            _portName = "COM4";
            _baudRate = 9600;
            _parity = Parity.None;
            _dataBits = 8;
            _stopBits = StopBits.One;
            _exchangeType = ExchangeType.RTU;

            //Харакатиристики TCP/IP
            _ipAddress = "127.0.0.0";
            _ipPort = 502;
        }
        public void CreateSerialPort()
        {
            _serialPort = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits);
        }
    }
    /// <summary>
    /// Тип узла
    /// </summary>
    enum NodeType
    {
        COM,TCP
    }
    /// <summary>
    /// Тип обмена данными
    /// </summary>
    enum ExchangeType
    {
        ASCII,RTU
    }
}
