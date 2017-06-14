using Horizoft.Relay.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Horizoft.Relay.DAL;

namespace Horizoft.Relay.Web.API
{
    public class RelayController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetReference()
        {
            string url = ConfigurationManager.AppSettings["URL"];

            try
            {
                IoTReferenceParser iotRefParser = new IoTReferenceParser();
                IoTReference iotRef = iotRefParser.Get(url);

                return Ok(iotRef);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        public IHttpActionResult GetCurrentState()
        {
            string url = ConfigurationManager.AppSettings["URL"] + "/state.xml";
            //string url = "http://relay.horizoft.prl/AppViews/Relay/state2.xml";

            try
            {
                IoTTransaction iotTrs = new IoTTransaction();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                XmlElement root = xmlDoc.DocumentElement;
                iotTrs.I1 = Convert.ToInt32(root["input1state"].InnerText);
                iotTrs.I2 = Convert.ToInt32(root["input2state"].InnerText);
                iotTrs.HighTime1 = Convert.ToDecimal(root["hightime1"].InnerText);
                iotTrs.HighTime2 = Convert.ToDecimal(root["hightime2"].InnerText);
                iotTrs.Count1 = Convert.ToInt32(root["count1"].InnerText);
                iotTrs.Count2 = Convert.ToInt32(root["count2"].InnerText);
                iotTrs.R1 = Convert.ToInt32(root["relay1state"].InnerText);
                iotTrs.R2 = Convert.ToInt32(root["relay2state"].InnerText);
                iotTrs.R3 = Convert.ToInt32(root["relay3state"].InnerText);
                iotTrs.R4 = Convert.ToInt32(root["relay4state"].InnerText);
                iotTrs.R5 = Convert.ToInt32(root["relay5state"].InnerText);
                iotTrs.R6 = Convert.ToInt32(root["relay6state"].InnerText);
                iotTrs.R7 = Convert.ToInt32(root["relay7state"].InnerText);
                iotTrs.R8 = Convert.ToInt32(root["relay8state"].InnerText);
                iotTrs.R9 = Convert.ToInt32(root["relay9state"].InnerText);
                iotTrs.R10 = Convert.ToInt32(root["relay10state"].InnerText);
                iotTrs.Units = root["units"].InnerText;
                iotTrs.T1 = ConvertToDecimal(root["sensor1temp"].InnerText);
                iotTrs.T2 = ConvertToDecimal(root["sensor2temp"].InnerText);
                iotTrs.ExtVar0 = ConvertToDecimal(root["extvar0"].InnerText);
                iotTrs.ExtVar1 = ConvertToDecimal(root["extvar1"].InnerText);
                iotTrs.ExtVar2 = ConvertToDecimal(root["extvar2"].InnerText);
                iotTrs.ExtVar3 = ConvertToDecimal(root["extvar3"].InnerText);
                iotTrs.ExtVar4 = ConvertToDecimal(root["extvar4"].InnerText);
                iotTrs.SerialNumber = root["serialNumber"].InnerText;
                iotTrs.CurrentDateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(root["time"].InnerText));
                iotTrs.CurrentDay = iotTrs.CurrentDateTime.ToString("dddd");
                iotTrs.CurrentDate = iotTrs.CurrentDateTime.ToString("dd MMMM yyyy");
                iotTrs.CurrentTime = iotTrs.CurrentDateTime.ToString("hh:mm:ss tt");

                return Ok(iotTrs);

            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private static Decimal ConvertToDecimal(string data)
        {
            Decimal parse_result;
            return (Decimal.TryParse(data, out parse_result)) ? parse_result : -300;
        }

    }
}
