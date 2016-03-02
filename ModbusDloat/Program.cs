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
            TestNode.portName = PortName.COM4;
            TestNode.CreatePort();
            server.Nodes.Add(TestNode);

            #region Device 1
            Device TestDevice = new Device();
            TestDevice.periodSurvey = TimeSpan.FromMilliseconds(2000);
            TestDevice.Name = "Тестоввый модуль";
            TestNode.Devices.Add(TestDevice);

            Tag TestTag = new Tag();
            TestTag.Name = "Температура";
            TestTag.dataType = DataType.FLOAT;
            TestTag.shuffleBytes = ShuffleBytes.HIGHER_WORD_AHEAD;
            TestTag.Address = 4;
            TestDevice.Tags.Add(TestTag);

            Tag TestTag2 = new Tag();
            TestTag2.Name = "Влажность";
            TestTag2.dataType = DataType.INT;
            TestTag2.shuffleBytes = ShuffleBytes.NONE;
            TestTag2.Address = 10;
            TestDevice.Tags.Add(TestTag2);

            Tag TestTag3 = new Tag();
            TestTag3.Name = "Давление";
            TestTag3.dataType = DataType.INT;
            TestTag3.shuffleBytes = ShuffleBytes.NONE;
            TestTag3.Address = 50;
            TestDevice.Tags.Add(TestTag3); 
            #endregion

            #region Device 2
            Device device2 = new Device();
            device2.Name = "Модуль ввода";
            device2.Address = 2;
            TestNode.Devices.Add(device2);

            Tag tag1 = new Tag();
            tag1.Address = 70;
            tag1.Name = "Тег 1 температура";
            tag1.dataType = DataType.FLOAT;
            device2.Tags.Add(tag1);

            Tag tag2 = new Tag();
            tag2.Address = 80;
            tag2.Name = "Тег 2 температура";
            tag2.dataType = DataType.FLOAT;
            device2.Tags.Add(tag2);

            Tag tag3 = new Tag();
            tag3.Address = 90;
            tag3.Name = "Тег 3 влажность";
            tag3.shuffleBytes = ShuffleBytes.NONE;
            tag3.dataType = DataType.INT;
            device2.Tags.Add(tag3); 
            #endregion

            #region Узел 2
            NodeCOM TestNode2 = new NodeCOM();
            TestNode2.Name = "УЗЕЛ 2";
            TestNode2.portName = PortName.COM5;
            TestNode2.CreatePort();
            server.Nodes.Add(TestNode2);

            Device device11 = new Device();
            device11.Name = device11.ToString();
            device11.Address = 1;
            TestNode2.Devices.Add(device11);

            Tag tag11 = new Tag();
            tag11.Address = 8;
            tag11.Name = "Тег 1 температура";
            tag11.dataType = DataType.INT;
            tag11.shuffleBytes = ShuffleBytes.NONE;
            device11.Tags.Add(tag11);

            Tag tag12 = new Tag();
            tag12.Address = 12;
            tag12.Name = "Тег 2 температура";
            tag12.dataType = DataType.INT;
            tag12.shuffleBytes = ShuffleBytes.NONE;
            device11.Tags.Add(tag12); 
            #endregion

            //NodeTCP TCPNode = new NodeTCP();
            //TCPNode.Name = "УЗЕЛ 2";
            //TCPNode.ipAddress = "192.168.0.130";
            //server.Nodes.Add(TCPNode);

            //Device TCPdevice = new Device();
            //TCPdevice.Name = TCPdevice.ToString();
            //TCPdevice.Address = 1;
            //TCPNode.Devices.Add(TCPdevice);

            //Tag TCPtag = new Tag();
            //TCPtag.Address = 8;
            //TCPtag.Name = "Тег 1 температура";
            //TCPtag.dataType = DataType.INT;
            //TCPtag.shuffleBytes = ShuffleBytes.NONE;
            //TCPdevice.Tags.Add(TCPtag);

            SurveyEngine surveyEngine = new SurveyEngine(server);

            surveyEngine.PrepareSurvey();
        }
    }
}
