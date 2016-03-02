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
            TestNode.CreateSerialPort();
            server.Nodes.Add(TestNode);

            Device TestDevice = new Device();
            TestDevice.periodSurvey = TimeSpan.FromMilliseconds(2000);
            TestNode.Devices.Add(TestDevice);

            Tag TestTag = new Tag();
            TestTag.dataType = DataType.FLOAT;
            TestTag.shuffleBytes = ShuffleBytes.HIGHER_WORD_AHEAD;
            TestTag.Address = 4;
            TestDevice.Tags.Add(TestTag);

            Tag TestTag2 = new Tag();
            TestTag2.dataType = DataType.INT;
            TestTag2.shuffleBytes = ShuffleBytes.NONE;
            TestTag2.Address = 10;
            TestDevice.Tags.Add(TestTag2);

            Tag TestTag3 = new Tag();
            TestTag3.dataType = DataType.INT;
            TestTag3.shuffleBytes = ShuffleBytes.NONE;
            TestTag3.Address = 50;
            TestDevice.Tags.Add(TestTag3);

            SurveyEngine surveyEngine = new SurveyEngine(server);

            surveyEngine.StartSurvey();
        }
    }
}
