using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizoft.Relay.DTO
{
    public class TemperatureReport
    {
        public string DateTime { get; set; }
        public List<SPTemperatureReport_Result> TemperatureReports { get; set; }

        public TemperatureReport()
        {

        }
    }
}
