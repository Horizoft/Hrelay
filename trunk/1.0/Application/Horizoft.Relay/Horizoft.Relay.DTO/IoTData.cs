using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizoft.Relay.DTO
{
    public class IoTData
    {
        public int I1 { get; set; }
        public int I2 { get; set; }
        public decimal HighTime1 { get; set; }
        public decimal HighTime2 { get; set; }
        public int Count1 { get; set; }
        public int Count2 { get; set; }
        public int R1 { get; set; }
        public int R2 { get; set; }
        public int R3 { get; set; }
        public int R4 { get; set; }
        public int R5 { get; set; }
        public int R6 { get; set; }
        public int R7 { get; set; }
        public int R8 { get; set; }
        public int R9 { get; set; }
        public int R10 { get; set; }
        public string Units { get; set; }
        public decimal T1 { get; set; }
        public decimal T2 { get; set; }
        public decimal T3 { get; set; }
        public decimal ExtVar0 { get; set; }
        public decimal ExtVar1 { get; set; }
        public decimal ExtVar2 { get; set; }
        public decimal ExtVar3 { get; set; }
        public decimal ExtVar4 { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public string CurrentDay { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentTime { get; set; }

        public IoTData() { }

    }
}
