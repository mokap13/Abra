using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class Tag:Device
    {
        /// <summary>
        /// Название тега
        /// </summary>
        public string _tagName { get; set; }
        /// <summary>
        /// Коменнтарий к тегу
        /// </summary>
        public string _comment { get; set; }
        /// <summary>
        /// Функция запроса протокола MODBUS
        /// </summary>
        public FunctionModbus _functionModbus { get; set; }
        /// <summary>
        /// Первый из запрашиваемых регистров
        /// </summary>
        public ushort _startAddress { get; set; }
        /// <summary>
        /// Кол-во запрашиваемых регистров
        /// </summary>
        public ushort _numberOfPoints { get; set; }
        /// <summary>
        /// Тип запрашиваемых данных
        /// </summary>
        public DataType _dataType { get; set; }
        /// <summary>
        /// Тип доступа
        /// </summary>
        public AccessType _accessType { get; set; }
        /// <summary>
        /// Перестановка байт(слов)
        /// </summary>
        public ShuffleBytes _shuffleBytes { get; set; }
        /// <summary>
        /// Полученные данные регистра
        /// </summary>
        public float[] _valueFloat;
        public ushort[] _valueUshort;
        public Tag()
        {
            _tagName = "Tag";
            _functionModbus = FunctionModbus.HOLDING_REGISTERS;
            _startAddress = 0;
            _numberOfPoints = 2;
            _dataType = DataType.FLOAT;
            _accessType = AccessType.READ_ONLY;
            _shuffleBytes = ShuffleBytes.HIGHER_WORD_AHEAD;
        }

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
    /// Тип запрашиваемых данных
    /// </summary>
    enum DataType
    {
        BOOL,INT,FLOAT,STRING
    }
    /// <summary>
    /// Тип доступа к данным
    /// </summary>
    enum AccessType
    {
        READ_ONLY, WRITE_ONLY, READ_WRITE
    }
    /// <summary>
    /// Перестановка байт(слов)
    /// </summary>
    enum ShuffleBytes
    {
        NONE, HIGHER_WORD_AHEAD , HIGHER_BYTE_AHEAD
    }
}
