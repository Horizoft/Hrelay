using Horizoft.EMail;
using Horizoft.Relay.DAL;
using Horizoft.Relay.DTO;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Horizoft.Relay.Monitoring
{
	public class MainClass
	{
        //private static string PATH_MAILTEMPLATE = @"/MailTemplate/MailMessage.html";
        private static string PATTERN_URL = "^(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]";
        private static string SENSOR_NAME = "S";
        private static string RELAY_NAME = "R";
        private static string RELAY_STATUS_ON = "ON";
        private static string RELAY_STATUS_OFF = "OFF";
        private static string SEARCH_LOG_TIME = "{LOG_TIME}";
        private static string SEARCH_RELAY = "{{RELAY_{0}}}";
        private static string SEARCH_SENSOR = "{{SENSOR_{0}}}";
        private static string REPORT_TIMESTAMP_FORMAT = "yyyy/MM/dd HH:mm:ss";

        private static int placeId = 1;
        private static int areaId = 1;

        //private static string SENSOR_1 = "S1";
        //private static int MAIL_SERVER_SMTP_TIMEOUT = 120000;
        private static EMailStructure mail = new EMailStructure();
        private static TemperatureDataLog temperatureDataLog;

		public static void Main(string[] args)
		{
            TemperatureData relayData = MonitorRelay(DateTime.Now);
            //Decimal currentTemperature = GetCurrentTemperature(relayData);
            Decimal currentTemperature = relayData.S1 ?? 50; ;

            if (IsCriticalTemperature(currentTemperature))
            {
                NotifyByMail(relayData);
            }

		}

        private static TemperatureData MonitorRelay(DateTime time)
        {
            string sourceLocation = ConfigurationManager.AppSettings["RELAY_LOG_LOCATION"];

            SourceType sourceType = (Regex.IsMatch(sourceLocation, PATTERN_URL)) ? SourceType.Url : SourceType.File;

            //temperatureDataLog = new TemperatureDataLog(SourceType.Url, sourceLocation);
            temperatureDataLog = new TemperatureDataLog(sourceType, sourceLocation, placeId, areaId);
            TemperatureData relayData = temperatureDataLog.GetLast();

            return relayData;
        }

        private static bool IsCriticalTemperature(Decimal temperature)
        {
            Decimal criticalTemperature = Convert.ToDecimal(ConfigurationManager.AppSettings["SENSOR_CRITICALTEMPERATURE"]); //celcius
            
            return (temperature >= criticalTemperature) ? true : false;
        }

        private static void NotifyByMail(TemperatureData relayData)
        {
            EMailStructure mail = ComposeMail(relayData);
            EMailService emailService = new EMailService();
            emailService.SendMailViaExchangeEWS(mail);
            emailService.SendMailViaSMTP(mail);
        }

        private static EMailStructure ComposeMail(TemperatureData relayData)
        {
            EMailStructure mail = new EMailStructure();

            mail.Sender = System.Configuration.ConfigurationManager.AppSettings["MAIL_MESSAGE_SENDER"];
            mail.Recipients = System.Configuration.ConfigurationManager.AppSettings["MAIL_MESSAGE_RECIPIENTS"].Split(';');
            mail.Subject = System.Configuration.ConfigurationManager.AppSettings["MAIL_MESSAGE_SUBJECT"];
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = CreateRelayDataReportForMail(relayData);
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHTML = true;

            return mail;
        }

        private static string CreateRelayDataReportForMail(TemperatureData relayData)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["MAIL_MESSAGE_TEMPLATE_PATH"];
            string path = System.Configuration.ConfigurationManager.AppSettings["MAIL_MESSAGE_TEMPLATE_PATH"];
            string message = File.ReadAllText(path);

            message = message.Replace(SEARCH_LOG_TIME, relayData.MonitoredDateTime.ToString(REPORT_TIMESTAMP_FORMAT));

            message = message.Replace(string.Format(SEARCH_RELAY, 1), (relayData.R1 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 2), (relayData.R2 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 3), (relayData.R3 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 4), (relayData.R4 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 5), (relayData.R5 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 6), (relayData.R6 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 7), (relayData.R7 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 8), (relayData.R8 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 9), (relayData.R9 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 10), (relayData.R10 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);

            message = message.Replace(string.Format(SEARCH_SENSOR, 1), relayData.S1.ToString());
            message = message.Replace(string.Format(SEARCH_SENSOR, 2), relayData.S2.ToString());
            message = message.Replace(string.Format(SEARCH_SENSOR, 3), relayData.S3.ToString());

            return message;
        }

	}
}
