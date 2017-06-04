using Horizoft.Relay.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace Horizoft.Relay.Web.API
{
    public class RelayController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCurrentState()
        {
            string url = "http://controlair.bepetrothai.com/state.xml";

            try
            {
                IoTData iotData = new IoTData();

                //var request = WebRequest.Create(url);
                //var response = request.GetResponse();

                //if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                //{
                //    // Get the stream containing content returned by the server.
                //    Stream dataStream = response.GetResponseStream();
                //    // Open the stream using a StreamReader.
                //    StreamReader reader = new StreamReader(dataStream);

                //    XmlSerializer serializer = new XmlSerializer(typeof(datavalues));
                //    dataValues = (datavalues)serializer.Deserialize(reader);
                //}

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                XmlElement root = xmlDoc.DocumentElement;
                iotData.I1 = Convert.ToInt32(root["input1state"].InnerText);
                iotData.I2 = Convert.ToInt32(root["input2state"].InnerText);
                iotData.HighTime1 = Convert.ToDecimal(root["hightime1"].InnerText);
                iotData.HighTime2 = Convert.ToDecimal(root["hightime2"].InnerText);
                iotData.Count1 = Convert.ToInt32(root["count1"].InnerText);
                iotData.Count2 = Convert.ToInt32(root["count2"].InnerText);
                iotData.R1 = Convert.ToInt32(root["relay1state"].InnerText);
                iotData.R2 = Convert.ToInt32(root["relay2state"].InnerText);
                iotData.R3 = Convert.ToInt32(root["relay3state"].InnerText);
                iotData.R4 = Convert.ToInt32(root["relay4state"].InnerText);
                iotData.R5 = Convert.ToInt32(root["relay5state"].InnerText);
                iotData.R6 = Convert.ToInt32(root["relay6state"].InnerText);
                iotData.R7 = Convert.ToInt32(root["relay7state"].InnerText);
                iotData.R8 = Convert.ToInt32(root["relay8state"].InnerText);
                iotData.R9 = Convert.ToInt32(root["relay9state"].InnerText);
                iotData.R10 = Convert.ToInt32(root["relay10state"].InnerText);
                iotData.Units = root["units"].InnerText;
                iotData.T1 = Convert.ToDecimal(root["sensor1temp"].InnerText);
                iotData.T2 = Convert.ToDecimal(root["sensor2temp"].InnerText);
                iotData.ExtVar0 = Convert.ToDecimal(root["extvar0"].InnerText);
                iotData.ExtVar1 = Convert.ToDecimal(root["extvar1"].InnerText);
                iotData.ExtVar2 = Convert.ToDecimal(root["extvar2"].InnerText);
                iotData.ExtVar3 = Convert.ToDecimal(root["extvar3"].InnerText);
                iotData.ExtVar4 = Convert.ToDecimal(root["extvar4"].InnerText);
                iotData.SerialNumber = root["serialNumber"].InnerText;
                iotData.CurrentDateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(root["time"].InnerText));
                iotData.CurrentDay = iotData.CurrentDateTime.ToString("dddd");
                iotData.CurrentDate = iotData.CurrentDateTime.ToString("dd MMMM yyyy");
                iotData.CurrentTime = iotData.CurrentDateTime.ToString("hh:mm:ss tt");

                return Ok(iotData);

            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
