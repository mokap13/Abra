using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ModbusSurvey
{
    class NodeTCP
    {
        public TcpClient _tcpClient;
        /// <summary>
        /// IP адрес 
        /// </summary>
        public string _ipAddress;
        /// <summary>
        /// Номер порта
        /// </summary>
        public ushort _ipPort;

        public NodeTCP()
        {
            //Харакатиристики TCP/IP
            _ipAddress = "127.0.0.0";
            _ipPort = 502;
        }

        public void CreateTcpClient()
        {
            
        }
    }
}
