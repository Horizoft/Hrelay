using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Horizoft.Relay.DTO;
using Horizoft.Relay.DAL;

namespace Horizoft.Relay.DTO.LogToDb
{
	public class Program
	{
        private static TemperatureDataLog temperatureLog;
		private static TemperatureDataRepository temperatureRepository;

		private static string command = string.Empty;
		private static string sourceType = string.Empty;
		private static string sourceLocation = string.Empty;

        private static string PATTERN_DATETIME = @"(\d{2}\/\d{2}\/\d{4}\s\d{2}:\d{2}:\d{2})";
		private static string PATTERN_COMMAND_ACTION = "(-import)|(-reset)";
		private static string PATTERN_COMMAND_SOURCETYPE = "(-u)|(-f)";
        //private static string LOG_DATETIME_FORMAT = "MM/dd/yyyy HH:mm:ss";

        private static string REPORT_DATE_FORMAT = "yyyy/MM/dd";
        private static string REPORT_TIMESTAMP_FORMAT = "yyyy/MM/dd HH:mm:ss";
        
        private static string REPORT_CALENDAR_LINE_01 = "{0}\tFound {1} days of temperature data not imported.";
        private static string REPORT_CALENDAR_LINE_02 = "{0}\t{1}. Importing date {2}";
        private static string REPORT_CALENDAR_LINE_03 = "\t\t\tDone.";

        private static string REPORT_LINE_01 = "{0}\tLoading temperature data from {1}";
		private static string REPORT_LINE_02 = "{0}\tLoading {1} lines of temperature log";
		private static string REPORT_LINE_03 = "{0}\tLast updated in database: {1}";
		private static string REPORT_LINE_04 = "{0}\tFound {1} new temperature data";
		private static string REPORT_LINE_05 = "{0}\tUpdated {1} new temperature data to database";

        private static string REPORT_RESET = "{0}\tLog reset is successfully";
		
        private static string ERROR_01 = "Usage: tempprogram.exe\r\n\t-import -f <file name>\r\n\t-import -u <http://url/csv.file>\r\n\t-reset -u <http://url/csv.file>";
		private static string ERROR_02 = "Source file not found or has no data";

        private static int placeId = -1;
        private static int areaId = -1;
        private static int errorCode = 0;

		public static void Main(string[] args)
		{
			List<string> lines = new List<string>();
            //args = new string[] { "-import", "-f", "log.txt" };

			placeId = 1;
			areaId = 1;

            temperatureLog = new TemperatureDataLog();
            temperatureRepository = new TemperatureDataRepository(placeId, areaId);

            if (CheckCLIUsage(args))
            {
                Console.WriteLine(REPORT_LINE_01, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), sourceLocation);

                switch (command)
                {
                    case "-reset":
                        switch (sourceType)
                        {
                            case "-u":
                                temperatureLog = new TemperatureDataLog(SourceType.Url, sourceLocation, placeId, areaId);
                                ResetLog();
                                break;

                            default:
                                errorCode = 1;
                                break;

                        }
                        break;

                    case "-import":
                        switch (sourceType)
                        {
                            case "-f":
                                //lines = temperatureLog.GetLogFile(sourceLocation);
                                //lines = temperatureLog.GetAll(SourceType.File, sourceLocation);
                                temperatureLog = new TemperatureDataLog(SourceType.File, sourceLocation, placeId, areaId);

                                if (!temperatureLog.HasData) errorCode = 2;
                                else StartImportLogToDatabase();

                                break;

                            case "-u":
                                //lines = temperatureLog.GetLogFromUrl(sourceLocation);
                                //lines = temperatureLog.GetAll(SourceType.Url, sourceLocation);
                                temperatureLog = new TemperatureDataLog(SourceType.Url, sourceLocation, placeId, areaId);

                                if (!temperatureLog.HasData) errorCode = 2;
                                else StartImportLogToDatabase();

                                break;

                            default:
                                errorCode = 1;
                                break;
                        }
                        break;

                    default:
                        errorCode = 1;
                        break;
                }

            }

            switch (errorCode)
            {
                case 1:
                    Console.WriteLine(ERROR_01);
                    return;

                case 2:
                    Console.WriteLine(ERROR_02);
                    return;
            }

		}

		public static bool CheckCLIUsage(string[] args)
		{
			errorCode = 1;

			if (args == null || args.Length < 3) return false;

			command = args[0].Trim();
			sourceType = args[1].Trim();
			sourceLocation = args[2].Trim();

			if (!Regex.IsMatch(command, PATTERN_COMMAND_ACTION)) return false;
			if (!Regex.IsMatch(sourceType, PATTERN_COMMAND_SOURCETYPE)) return false;

			errorCode = 0;

			return true;
		}

        public static void ResetLog()
        {
            List<TemperatureData> temperatureDataList = new List<TemperatureData>();

            temperatureLog.Delete(sourceLocation);
            temperatureDataList = temperatureLog.GetAll();

            if (temperatureDataList.Count == 1) Console.WriteLine(REPORT_RESET, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT));
        }


        public static void StartImportLogToDatabase()
        {
            int n_loadedLines = 0;
            int n_newlines = 0;

            //if (lines != null && lines.Count > 0)
            if (temperatureLog.HasData)
            {
                List<TemperatureData> temperatureDataList = temperatureLog.GetAll();
                n_loadedLines = temperatureDataList.Count;
                Console.WriteLine(REPORT_LINE_02, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), n_loadedLines);

                DateTime lastDateTimeInDatabase = temperatureRepository.GetLastDateTime();
                Console.WriteLine(REPORT_LINE_03, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), lastDateTimeInDatabase.ToString(REPORT_DATE_FORMAT));

                temperatureDataList = temperatureLog.GetLatestData(lastDateTimeInDatabase);
                n_newlines = temperatureDataList.Count;
                Console.WriteLine(REPORT_LINE_04, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), n_newlines);

                if (n_newlines > 0)
                {
                    //List<TemperatureData> temperatureDataList = ConvertLogToTemperatureData(placeId, areaId, lines);

                    DateTime firstDateTimeInLog = temperatureDataList.First().MonitoredDateTime;

                    if (IsSameDay(lastDateTimeInDatabase, firstDateTimeInLog))
                        ImportSameDay(temperatureDataList);
                    else
                        ImportManyDays(temperatureDataList);

                    Console.WriteLine(REPORT_LINE_05, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), n_newlines);
                }
            }

        }

        private static void ImportSameDay(List<TemperatureData> temperatureDataList)
        {
            temperatureRepository.Add(temperatureDataList);
        }

        private static void ImportManyDays(List<TemperatureData> temperatureDataList)
        {
            //var monitoredDate = temperatureDataList.GroupBy(l => l.MonitoredDate).Select(n => new { Value = n.Count() });
            var monitoredDateList = temperatureDataList.GroupBy(l => l.MonitoredDate).ToList();
            int countMonitoredDate = monitoredDateList.Count();

            Console.WriteLine(REPORT_CALENDAR_LINE_01, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), countMonitoredDate);

            //foreach(var monitoredDate in monitoredDateList)

            for (int n = 0; n < monitoredDateList.Count(); n++)
            {
                List<TemperatureData> _temperatureDataList = temperatureDataList.Where(l => l.MonitoredDate == monitoredDateList[n].Key).ToList();

                Console.Write(REPORT_CALENDAR_LINE_02, DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT), (n + 1).ToString().PadLeft(3, ' '), monitoredDateList[n].Key.ToString(REPORT_DATE_FORMAT));

                temperatureRepository.Add(_temperatureDataList);

                Console.WriteLine(REPORT_CALENDAR_LINE_03);

            }
        }

        private static bool IsSameDay(DateTime dateInDatabase, DateTime dateInLog)
        {
            //int diff_days = DateTime.Compare(dateTime1.Date, dateTime2.Date);
            double diff_hours = (dateInLog - dateInDatabase).TotalHours;

            return (diff_hours > 24) ? false : true;
        }
        
        private static List<TemperatureData> ConvertLogToTemperatureData(int placeId, int areaId, List<string> lines)
        {
            TemperatureDataRepository temperatureRepository = new TemperatureDataRepository(placeId, areaId);

            if (lines == null || lines.Count == 0) return null;

            string[] logProperties;
            string[] logData;

            logProperties = lines[0].Split(',');
            int logPropertiesCount = logProperties.Count();

            lines.RemoveAt(0);

            List<TemperatureData> temperatureDataList = new List<TemperatureData>();

            foreach (string line in lines)
            {
                logData = line.Split(',');
                logData = HandleDataExceedProperties(logData, logPropertiesCount);
                temperatureDataList.Add(temperatureLog.SetData(placeId, areaId, logData));
                //temperatureRepository.Add(rowColumn);
            }

            return temperatureDataList;
        }

        private static string[] HandleDataExceedProperties(string[] logData, int logPropertiesCount)
        {
            if (logData.Count() <= logPropertiesCount) return logData;

            int n_logProperties = logPropertiesCount;
            int n_logData = 0;


            string[] newLogData = new string[n_logProperties];

            Array.Copy(logData, newLogData, n_logProperties);

            for (n_logData = n_logProperties; n_logData < logData.Count(); n_logData++)
            {
                newLogData[n_logProperties - 1] += logData[n_logData];
            }

            return newLogData;
        }
	}
}
