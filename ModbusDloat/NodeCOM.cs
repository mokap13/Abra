using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class NodeCOM
    {
        /// <summary>
        /// Имя узла
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Комментарий узла
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Список устройств узла
        /// </summary>
        public List<Device> Devices;
        /// <summary>
        /// Последовательный порт
        /// </summary>
        public SerialPort serialPort { get; set; }
        /// <summary>
        /// Имя порта
        /// </summary>
        public PortName portName { get; set; }
        /// <summary>
        /// Скорость порта
        /// </summary>
        public BaudRate baudRate { get; set; }
        /// <summary>
        /// Четность порта
        /// </summary>
        public Parity parity { get; set; }
        /// <summary>
        /// Длина адреса порта
        /// </summary>
        public ushort dataBits { get; set; }
        /// <summary>
        /// Стоп-биты
        /// </summary>
        public StopBits stopBits { get; set; }

        /// <summary>
        /// Конструктор с настройками "по умолчанию"
        /// </summary>
        public NodeCOM()
        {
            Devices = new List<Device>();
            //Характеристики узла
            Name = "Узел";

            //Характеристики COM порта
            portName = PortName.COM4;
            baudRate = BaudRate._9600;
            parity = Parity.None;
            dataBits = 8;
            stopBits = StopBits.One;
        }
        /// <summary>
        /// Создает последовательный порт с настройками объекта NodeCom
        /// </summary>
        public void CreateSerialPort()
        {
            serialPort = new SerialPort(portName.ToString(), (int)baudRate, parity, dataBits, stopBits);
        }
    }
    /// <summary>
    /// Имя порта (COM1,COM2...)
    /// </summary>
    enum PortName
    {
        COM1,COM2,COM3,COM4,COM5,COM6,COM7,COM8,COM9,COM10,
        COM11,COM12,COM13,COM14,COM15,COM16,COM17,COM18,COM19,COM20
    }
    /// <summary>
    /// Сокрость COM порта
    /// </summary>
    enum BaudRate
    {
        _1200 = 1200,
        _1800 = 1800,
        _2400 = 2400,
        _4800 = 4800,
        _9600 = 9600,
        _19200 = 19200,
        _38400 = 38400,
        _57600 = 57600,
        _115200 = 115200
    }
}
