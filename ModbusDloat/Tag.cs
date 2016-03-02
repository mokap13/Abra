using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    class Tag : IDescription
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
        /// Тип запрашиваемых данных
        /// </summary>
        public DataType dataType { get; set; }
        /// <summary>
        /// Перестановка байт(слов)
        /// </summary>
        public ShuffleBytes shuffleBytes { get; set; }
        /// <summary>
        /// Полученные данные регистра
        /// </summary>
        public float valueFloat;
        public ushort valueUshort;
        public bool valueBool;
        
        public ushort Address;
        public Tag()
        {
            Name = "Tag";
            
            dataType = DataType.FLOAT;
            
            shuffleBytes = ShuffleBytes.HIGHER_WORD_AHEAD;
        }

    }
    
    /// <summary>
    /// Тип запрашиваемых данных
    /// </summary>
    enum DataType
    {
        BOOL, INT, FLOAT, STRING
    }
 
    /// <summary>
    /// Перестановка байт(слов)
    /// </summary>
    enum ShuffleBytes
    {
        NONE, HIGHER_WORD_AHEAD, HIGHER_BYTE_AHEAD
    }
}
