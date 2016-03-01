using System;
using Modbus.Device;
using FB;
using System.Runtime.InteropServices;
using System.ComponentModel;
using InSAT.Library.Interop;
using System.IO.Ports;

namespace FBModbusTest
{
    [Serializable,
        ComVisible(true),
        Guid("823A9765-0B26-45B2-97C2-FF7A5403F160"),
        CatID(CatIDs.CATID_OTHER),
        DisplayName("MODBUSSurvey")]
    public class FBModbusSurvey : StaticFBBase
    {
        const int PIN_1 = 1;
        const int PIN_2 = 2;
        const int PIN_3 = 3;
        const int PIN_4 = 4;

        protected override void UpdateData()
        {
            ushort[] DataArr = ModbusRegistersSurvey();
            SetPinValue(PIN_1, (int)DataArr[1]);
            SetPinValue(PIN_2, (int)DataArr[2]);
            SetPinValue(PIN_3, (int)DataArr[3]);
        }

        public ushort[] ModbusRegistersSurvey()
        {
            ushort[] registers = new ushort[2];

            SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            port.Open();

            ModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

            byte SlaveID = 1;
            ushort startAddress = 1;
            ushort pointsAddress = 3;

            registers = master.ReadHoldingRegisters(SlaveID, startAddress, pointsAddress);

            return registers;
        }
       
    }
}
