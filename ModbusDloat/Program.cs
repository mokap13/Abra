using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Modbus.IO;
using Modbus.Device;
using System.IO.Ports;

namespace ModbusSurvey
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            NodeCOM TestNode = new NodeCOM();
            server.Nodes.Add(TestNode);
            TestNode.portName = PortName.COM4;
            TestNode.CreateSerialPort();
            TestNode.serialPort.Open();

            Device TestDevice = new Device();
            TestNode.Devices.Add(TestDevice);
            TestDevice.periodSurvey = TimeSpan.FromMilliseconds(2000);
            TestDevice.CreateModbusMaster(TestNode);

            Tag TestTag = new Tag();
            TestTag.dataType = DataType.INT;
            TestTag.shuffleBytes = ShuffleBytes.NONE;
            TestTag.startAddress = 0;
            TestTag.numberOfPoints = 50;
            TestDevice.Tags.Add(TestTag);

            #region TestTags
            //Tag TestTag_0 = new Tag();
            //TestTag_0.startAddress = 4;
            //TestTag_0._tagName = "Температура(ближняя) зал 1";
            //TestDevice.Tags.Add(TestTag_0);

            //Tag TestTag_1 = new Tag();
            //TestTag_1._tagName = "Влажность зал 1";
            //TestTag_1.startAddress = 10;
            //TestDevice.Tags.Add(TestTag_1);

            //Tag TestTag_2 = new Tag();
            //TestTag_2._tagName = "Температура(средняя) зал 1";
            //TestTag_2.startAddress = 16;
            //TestDevice.Tags.Add(TestTag_2);

            //Tag TestTag_3 = new Tag();
            //TestTag_3._tagName = "Температура(дальняя) зал 1";
            //TestTag_3.startAddress = 22;
            //TestDevice.Tags.Add(TestTag_3);

            //Tag TestTag_4 = new Tag();
            //TestTag_4._tagName = "Контроль фаз";
            //TestTag_4.startAddress = 28;
            //TestDevice.Tags.Add(TestTag_4);

            //Tag TestTag_5 = new Tag();
            //TestTag_5._tagName = "Влажность зал 2";
            //TestTag_5.startAddress = 34;
            //TestDevice.Tags.Add(TestTag_5);

            //Tag TestTag_6 = new Tag();
            //TestTag_6._tagName = "Температура(средняя) зал 2";
            //TestTag_6.startAddress = 40;
            //TestDevice.Tags.Add(TestTag_6);

            //Tag TestTag_7 = new Tag();
            //TestTag_7._tagName = "Температура(ближняя) зал 2";
            //TestTag_7.startAddress = 46;
            //TestDevice.Tags.Add(TestTag_7); 
            #endregion

            //CreateConnection(TestNode, TestDevice);

            SurveyEngine surveyEngine = new SurveyEngine(server);

            while (true)
            {
                surveyEngine.StartSurvey();
            }
        }

        public static void CreateConnection(NodeCOM node, Device device)
        {
            //node.CreateSerialPort();
            //node._serialPort.Open();
            //device.CreateModbusMaster(node);
            
            //device.deviceMaster = ModbusSerialMaster.CreateRtu(node._serialPort);
        }
    }
}
