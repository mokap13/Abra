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

            Node TestNode = new Node();
            server.Nodes.Add(TestNode);
            TestNode._portName = "COM4";
            TestNode.CreateSerialPort();
            TestNode._serialPort.Open();

            Device TestDevice = new Device();
            TestNode.Devices.Add(TestDevice);
            TestDevice._periodSurvey = TimeSpan.FromMilliseconds(2000);
            TestDevice._deviceName = "Модуль ввода 1";
            TestDevice.CreateModbusMaster(TestNode);

            Tag TestTag = new Tag();
            TestTag._dataType = DataType.INT;
            TestTag._shuffleBytes = ShuffleBytes.NONE;
            TestTag._startAddress = 0;
            TestTag._numberOfPoints = 50;
            TestTag._tagName = "Температура(ближняя) зал 1";
            TestDevice.Tags.Add(TestTag);

            #region TestTags
            //Tag TestTag_0 = new Tag();
            //TestTag_0._startAddress = 4;
            //TestTag_0._tagName = "Температура(ближняя) зал 1";
            //TestDevice.Tags.Add(TestTag_0);

            //Tag TestTag_1 = new Tag();
            //TestTag_1._tagName = "Влажность зал 1";
            //TestTag_1._startAddress = 10;
            //TestDevice.Tags.Add(TestTag_1);

            //Tag TestTag_2 = new Tag();
            //TestTag_2._tagName = "Температура(средняя) зал 1";
            //TestTag_2._startAddress = 16;
            //TestDevice.Tags.Add(TestTag_2);

            //Tag TestTag_3 = new Tag();
            //TestTag_3._tagName = "Температура(дальняя) зал 1";
            //TestTag_3._startAddress = 22;
            //TestDevice.Tags.Add(TestTag_3);

            //Tag TestTag_4 = new Tag();
            //TestTag_4._tagName = "Контроль фаз";
            //TestTag_4._startAddress = 28;
            //TestDevice.Tags.Add(TestTag_4);

            //Tag TestTag_5 = new Tag();
            //TestTag_5._tagName = "Влажность зал 2";
            //TestTag_5._startAddress = 34;
            //TestDevice.Tags.Add(TestTag_5);

            //Tag TestTag_6 = new Tag();
            //TestTag_6._tagName = "Температура(средняя) зал 2";
            //TestTag_6._startAddress = 40;
            //TestDevice.Tags.Add(TestTag_6);

            //Tag TestTag_7 = new Tag();
            //TestTag_7._tagName = "Температура(ближняя) зал 2";
            //TestTag_7._startAddress = 46;
            //TestDevice.Tags.Add(TestTag_7); 
            #endregion

            //CreateConnection(TestNode, TestDevice);

            SurveyEngine surveyEngine = new SurveyEngine(server);

            while (true)
            {
                surveyEngine.StartSurvey();
            }
        }

        public static void CreateConnection(Node node, Device device)
        {
            //node.CreateSerialPort();
            //node._serialPort.Open();
            //device.CreateModbusMaster(node);
            
            //device._deviceMaster = ModbusSerialMaster.CreateRtu(node._serialPort);
        }
    }
}
