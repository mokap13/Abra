using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ModbusSurvey
{
    class NodeTCP:Node
    {
       // public List<Device> Devices;
        //public string Name { get; set; }
        //public string Comment { get; set; }
        public TcpClient tcpClient;
        /// <summary>
        /// IP адрес 
        /// </summary>
        public string ipAddress;
        /// <summary>
        /// Номер порта
        /// </summary>
        public ushort ipPort;

        public NodeTCP()
        {
            //Харакатиристики TCP/IP
            ipAddress = "192.168.42.198";
            ipPort = 502;

            Devices = new List<Device>();
        }

        public override void CreatePort()
        {
            if (tcpClient == null)
            {
                tcpClient = new TcpClient(); 
            }
        }
        public override void OpenPort()
        {
                tcpClient.BeginConnect(ipAddress, ipPort, null, null); 
        }
    }
}
