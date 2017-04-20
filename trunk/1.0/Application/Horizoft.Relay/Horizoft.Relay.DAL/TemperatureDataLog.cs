using Horizoft.Relay.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Horizoft.Relay.DAL
{
    public enum SourceType { File, Url };
	public class TemperatureDataLog
	{
        //private static string PATTERN_DATETIME = @"(\d{2}\/\d{2}\/\d{4}\s\d{2}:\d{2}:\d{2})";
		private const string PATTERN_DATETIME = @"(\d{2}\/\d{2}\/\d{4}\s\d{2}:\d{2}:\d{2})";
		private const string LOG_CSV_FORMAT = @"(\d{2}\/\d{2}\/\d{4}\s\d{2}:\d{2}:\d{2}),\d+,\d+,\d+,\d+,\d+,\d+,\d+,\d+,\d+,\d+,\d+,\d+,((\d+\.\d+)|(\w+\.\w+)),((\d+\.\d+)|(\w+\.\w+)),((\d+\.\d+)|(\w+\.\w+)),[\w\(\)\:\,\s]+";
        private const string LOG_DATETIME_FORMAT = "MM/dd/yyyy HH:mm:ss";
        private const string REPORT_DATETIME_FORMAT = "yyyy/MM/dd HH:mm:ss";
		private const string COMMAND_RESET = "{0}?erase=1";
        private string header = string.Empty;
        private List<string> lines = new List<string>();
        private List<TemperatureData> temperatureDataList = new List<TemperatureData>();

		public TemperatureDataLog()
		{

		}

        public TemperatureDataLog(SourceType sourceType, string sourceLocation, int placeId, int areaId)
        {
            temperatureDataList = GetAll(sourceType, sourceLocation, placeId, areaId);
        }

        public bool HasData
        {
            get
            {
                return (temperatureDataList != null && temperatureDataList.Count > 0) ? true : false;
            }
        }

        public List<TemperatureData> GetAll()
        {
            return temperatureDataList;
        }

        private List<TemperatureData> GetAll(SourceType sourceType, string sourceLocation, int placeId, int areaId)
        {
            List<TemperatureData> temperatureDataList = new List<TemperatureData>();
            List<string> log = GetAll(sourceType, sourceLocation);

            if (log == null) return null;

            foreach (string logData in log)
            {
                TemperatureData temperatureData = SetData(placeId, areaId, logData.Split(','));
                temperatureDataList.Add(temperatureData);
            }

            return temperatureDataList.OrderBy(l=>l.MonitoredDateTime).ToList();
        }

        private List<string> GetAll(SourceType sourceType, string sourceLocation)
        {
            if (!string.IsNullOrEmpty(sourceLocation))
            {
                if (sourceType == SourceType.Url) return GetLogFromUrl(sourceLocation);
                if (sourceType == SourceType.File) return GetLogFile(sourceLocation);
            }

            return null;
        }

		private List<string> GetLogFromUrl(string location)
		{
			WebClient client = new WebClient();

			try
			{
				Stream stream = client.OpenRead(location);
				using (StreamReader reader = new StreamReader(stream))
				{
					string line;
					line = reader.ReadToEnd();

					lines = line.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList<string>();
				}

			}
			catch (Exception ex)
			{

			}

            VerifyLogData();

			return lines;
		}

        private List<string> GetLogFile(string location)
		{
            //List<string> lines = new List<string>();
			string csvData = string.Empty;

			if (!File.Exists(location))
			{
				return null;
			}

            try
            {
                using (StreamReader reader = new StreamReader(location))
                {
                    string line;
                    line = reader.ReadToEnd();

                    lines = line.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList<string>();
                    header = lines[0];
                }

            }
            catch (Exception ex)
            {

            }

            VerifyLogData();

            return lines;
		}

        public List<TemperatureData> GetLatestData(DateTime currentTimestamp)
        {
            if (currentTimestamp == null) return null;

            DateTime firstTimestamp = temperatureDataList.First().MonitoredDateTime;
            DateTime lastTimestamp = temperatureDataList.Last().MonitoredDateTime;
            string datetime = currentTimestamp.ToString(LOG_DATETIME_FORMAT);

            Console.WriteLine("{0}{1}{2}", " ".PadRight(18), "\tLatest timestamp in database => ", datetime);
            Console.WriteLine("{0}{1}{2}", " ".PadRight(18), "\t      First timestamp in log => ", firstTimestamp.ToString(REPORT_DATETIME_FORMAT));
            Console.WriteLine("{0}{1}{2}", " ".PadRight(18), "\t       Last timestamp in log => ", lastTimestamp.ToString(REPORT_DATETIME_FORMAT));

            return temperatureDataList.Where(l => l.MonitoredDateTime > currentTimestamp).OrderBy(l => l.MonitoredDateTime).ToList();
        }

        public TemperatureData GetLast()
        {
            return temperatureDataList.Last();
        }

        public TemperatureData SetData(int placeId, int areaId, string[] logData)
        {
            string LOG_DATETIME_FORMAT = ConfigurationManager.AppSettings["LOG_DATETIME_FORMAT"];

            TemperatureData tempData = new TemperatureData();
            string[] datetime = new string[] { string.Empty, string.Empty };

            if (Regex.IsMatch(logData[0], PATTERN_DATETIME)) datetime = logData[0].Split(' ');

            //DateTime dt1 = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dt2 = DateTime.ParseExact("01/24/2013", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //tempData.MonitoredDateTime = Convert.ToDateTime(logData[0]);
            //tempData.MonitoredDate = Convert.ToDateTime(datetime[0]);

            //tempData.MonitoredDateTime = DateTime.ParseExact(logData[0], LOG_DATETIME_FORMAT, CultureInfo.InvariantCulture);
            tempData.MonitoredDate = DateTime.ParseExact(datetime[0], LOG_DATETIME_FORMAT, CultureInfo.InvariantCulture);
            tempData.MonitoredTime = TimeSpan.Parse(datetime[1]);
            tempData.MonitoredDateTime = tempData.MonitoredDate + tempData.MonitoredTime;
            tempData.PlaceId = placeId;
            tempData.AreaId = areaId;
            tempData.I1 = ConvertToInt(logData[1]);
            tempData.I2 = ConvertToInt(logData[2]);
            tempData.R1 = ConvertToInt(logData[3]);
            tempData.R2 = ConvertToInt(logData[4]);
            tempData.R3 = ConvertToInt(logData[5]);
            tempData.R4 = ConvertToInt(logData[6]);
            tempData.R5 = ConvertToInt(logData[7]);
            tempData.R6 = ConvertToInt(logData[8]);
            tempData.R7 = ConvertToInt(logData[9]);
            tempData.R8 = ConvertToInt(logData[10]);
            tempData.R9 = ConvertToInt(logData[11]);
            tempData.R10 = ConvertToInt(logData[12]);
            tempData.S1 = ConvertToDecimal(logData[13]);
            tempData.S2 = ConvertToDecimal(logData[14]);
            tempData.S3 = ConvertToDecimal(logData[15]);
            tempData.Source = logData[16].TrimEnd();

            return tempData;
        }

        public void Delete(string url)
        {
            WebClient client = new WebClient();

            try
            {
                string urlReset = string.Format(COMMAND_RESET, url);
                Stream stream = client.OpenRead(urlReset);
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    line = reader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void VerifyLogData()
        {
            try
            {
                int n = 0;
                while (n < lines.Count)
                {
                    string line = lines[n];

                    if (Regex.IsMatch(line, LOG_CSV_FORMAT)) n++;
                    else lines.RemoveAt(n);

                }
            }
            catch { }
        }

        private static int ConvertToInt(string data)
        {
            Int16 parse_result;
            return (Int16.TryParse(data, out parse_result)) ? parse_result : -1;
        }

        private static Decimal ConvertToDecimal(string data)
        {
            Decimal parse_result;
            return (Decimal.TryParse(data, out parse_result)) ? parse_result : -1000;
        }

	}
}
