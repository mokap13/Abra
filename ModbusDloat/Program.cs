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
            TestDevice.startAddress = 4;
            TestDevice.numberOfPoints = 46;
            TestNode.Devices.Add(TestDevice);

            Tag TestTag = new Tag();
            TestTag.dataType = DataType.FLOAT;
            TestTag.shuffleBytes = ShuffleBytes.HIGHER_WORD_AHEAD;
            TestDevice.Tags.Add(TestTag);

            SurveyEngine surveyEngine = new SurveyEngine(server);

            surveyEngine.StartSurvey();
        }
    }
}
