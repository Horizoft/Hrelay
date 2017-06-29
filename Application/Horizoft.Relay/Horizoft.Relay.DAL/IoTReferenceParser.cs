using Horizoft.Relay.DTO;
using System.Net;
using System.Text.RegularExpressions;

namespace Horizoft.Relay.DAL
{
    public class IoTReferenceParser
    {
        private string PATTERN_RELAY_NAME = @"var\s+d\s*=\s*\[["",\s\w-_!@#$%&]+\];";
        private string PATTERN_SENSOR_NAME = @"var\s+s\s*=\s*\[["",\s\w-_!@#$%&]+\];";
        private string PATTERN_RELAY_OPERATION = @"var\s+rs\s*=\s*\[["",\s\w-_!@#$%&]+\];";
        private string PATTERN_SENSOR_OPERATION = @"var\s+sr\s*=\s*\[["",\s\w-_!@#$%&]+\];";
        private string PATTERN_REMOVED_CHAR = @"[\[\]""]";

        private string[] relayName;
        private string[] sensorName;
        private string[] relayOperation;
        private string[] sensorOperation;

        public IoTReferenceParser()
        {

        }

        private string LoadHtml(string url)
        {
            string html = string.Empty;

            using (WebClient client = new WebClient())
            {
                html = client.DownloadString(url);
            }

            return html;
        }

        private void ParseReferenceData(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                string relayNameRawData = Regex.Match(html, PATTERN_RELAY_NAME).Value;
                string sensorNameRawData = Regex.Match(html, PATTERN_SENSOR_NAME).Value;
                string relayOperationRawData = Regex.Match(html, PATTERN_RELAY_OPERATION).Value;
                string sensorOperationRawData = Regex.Match(html, PATTERN_SENSOR_OPERATION).Value;

                relayName = Regex.Replace(relayNameRawData, PATTERN_REMOVED_CHAR, "").Split('=')[1].Trim().Split(',');
                sensorName = Regex.Replace(sensorNameRawData, PATTERN_REMOVED_CHAR, "").Split('=')[1].Trim().Split(',');
                relayOperation = Regex.Replace(relayOperationRawData, PATTERN_REMOVED_CHAR, "").Split('=')[1].Trim().Split(',');
                sensorOperation = Regex.Replace(sensorOperationRawData, PATTERN_REMOVED_CHAR, "").Split('=')[1].Trim().Split(',');

            }

        }

        private IoTReference SetReferenceData()
        {
            IoTReference iotReference = new IoTReference();

            if (relayName != null)
            {
                iotReference.Relay1Name = relayName[1];
                iotReference.Relay2Name = relayName[2];
                iotReference.Relay3Name = relayName[3];
                iotReference.Relay4Name = relayName[4];
                iotReference.Relay5Name = relayName[5];
                iotReference.Relay6Name = relayName[6];
                iotReference.Relay7Name = relayName[7];
                iotReference.Relay8Name = relayName[8];
                iotReference.Relay9Name = relayName[9];
                iotReference.Relay10Name = relayName[10];
            }

            if (sensorName != null)
            {
                iotReference.Sensor1Name = sensorName[1];
                iotReference.Sensor2Name = sensorName[2];
                iotReference.Sensor3Name = sensorName[3];
            }

            if (relayOperation != null)
            {
                iotReference.IsRelay1Operate = (relayOperation[1] == "1") ? true : false;
                iotReference.IsRelay2Operate = (relayOperation[2] == "1") ? true : false;
                iotReference.IsRelay3Operate = (relayOperation[3] == "1") ? true : false;
                iotReference.IsRelay4Operate = (relayOperation[4] == "1") ? true : false;
                iotReference.IsRelay5Operate = (relayOperation[5] == "1") ? true : false;
                iotReference.IsRelay6Operate = (relayOperation[6] == "1") ? true : false;
                iotReference.IsRelay7Operate = (relayOperation[7] == "1") ? true : false;
                iotReference.IsRelay8Operate = (relayOperation[8] == "1") ? true : false;
                iotReference.IsRelay9Operate = (relayOperation[9] == "1") ? true : false;
                iotReference.IsRelay10Operate = (relayOperation[10] == "1") ? true : false;
            }

            if (sensorOperation != null)
            {
                iotReference.IsSensor1Operate = (sensorOperation[0] == "1") ? true : false;
                iotReference.IsSensor2Operate = (sensorOperation[1] == "1") ? true : false;
                iotReference.IsSensor3Operate = (sensorOperation[2] == "1") ? true : false;
            }


            return iotReference;
        }

        public IoTReference Get(string url)
        {
            IoTReference iotReference = new IoTReference();

            string html = LoadHtml(url);
            ParseReferenceData(html);
            iotReference = SetReferenceData();

            return iotReference;
        }

    }
}
