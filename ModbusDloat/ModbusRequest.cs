using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class ModbusRequest
    {
        
    }

    /// <summary>
    /// Конфигурация запроса
    /// </summary>
    public struct RequestConfig
    {
        public byte _slaveAddress;
        public ushort _startAddress;
        public ushort _numberOfPoints;
        /// <summary>
        /// Конструктор конфигурации опроса
        /// </summary>
        /// <param name="slaveAddress">Адрес устройства</param>
        /// <param name="startAddress">Адрес первого из запрашиваемых регистров</param>
        /// <param name="numberOfPoints">Кол-во опрашиваемых регистров относительно первого</param>
        public RequestConfig(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            _slaveAddress = slaveAddress;
            _startAddress = startAddress;
            _numberOfPoints = numberOfPoints;
        }
    }
    /// <summary>
    /// Тип запрашиваемой переменной
    /// </summary>
    enum typeValue
    {
        Int, Float, Bool, String
    }
}
