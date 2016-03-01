using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class Tag:IDescription
    {
        /// <summary>
        /// Название тега
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Коменнтарий к тегу
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Функция запроса протокола MODBUS
        /// </summary>
        public FunctionModbus functionModbus { get; set; }
        /// <summary>
        /// Первый из запрашиваемых регистров
        /// </summary>
        public ushort startAddress { get; set; }
        /// <summary>
        /// Кол-во запрашиваемых регистров
        /// </summary>
        public ushort numberOfPoints { get; set; }
        /// <summary>
        /// Тип запрашиваемых данных
        /// </summary>
        public DataType dataType { get; set; }
        /// <summary>
        /// Тип доступа
        /// </summary>
        public AccessType accessType { get; set; }
        /// <summary>
        /// Перестановка байт(слов)
        /// </summary>
        public ShuffleBytes shuffleBytes { get; set; }
        /// <summary>
        /// Полученные данные регистра
        /// </summary>
        public float[] valueFloat;
        public ushort[] valueUshort;
        public Tag()
        {
            Name = "Tag";
            functionModbus = FunctionModbus.HOLDING_REGISTERS;
            startAddress = 0;
            numberOfPoints = 2;
            dataType = DataType.FLOAT;
            accessType = AccessType.READ_ONLY;
            shuffleBytes = ShuffleBytes.HIGHER_WORD_AHEAD;
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
        BOOL, INT, FLOAT, STRING
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
        NONE, HIGHER_WORD_AHEAD, HIGHER_BYTE_AHEAD
    }
}
