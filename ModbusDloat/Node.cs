using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSurvey
{
    abstract class Node
    {
        /// <summary>
        /// Имя узла
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Комментарий узла
        /// </summary>
        public string Comment { get; set; }

        public List<Device> Devices;

        abstract public void CreatePort();

        abstract public void OpenPort();
    }
}
