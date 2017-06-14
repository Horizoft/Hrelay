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
        private static string RELAY_STATUS_ON = "ON";
        private static string RELAY_STATUS_OFF = "OFF";
        private static string SEARCH_LOG_TIME = "{LOG_TIME}";
        private static string SEARCH_RELAY = "{{RELAY_{0}}}";
        private static string SEARCH_SENSOR = "{{SENSOR_{0}}}";
        private static string REPORT_TIMESTAMP_FORMAT = "yyyy/MM/dd HH:mm:ss";

        private static decimal CRITICAL_TEMPERATURE_DEFAULT = 25;

        private static EMailStructure mail = new EMailStructure();
        //private static TemperatureDataLog temperatureDataLog;
        private static Monitor monitorDto;
        

		public static void Main(string[] args)
		{
            MonitorRepository monitorDal = new MonitorRepository();
            monitorDto = monitorDal.GetLast();
            
            IoTTransactionParser iotTrsParser = new IoTTransactionParser();
            IoTTransaction iotTrsData = iotTrsParser.GetCurrentState();

            Decimal currentTemperature = iotTrsData.T1;

            if (IsCriticalTemperature(currentTemperature))
            {
                Console.WriteLine("{0}Alert by e-mail", " ".PadLeft(25, ' '));
                //NotifyByMail(iotTrsData);
                Console.WriteLine("{0}Mail sent.", DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT).PadRight(25, ' '));
            }
            else
            {

            }

		}


        private static bool IsCriticalTemperature(Decimal temperature)
        {
            Console.WriteLine("{0}Current temperature is {1} C", DateTime.Now.ToString(REPORT_TIMESTAMP_FORMAT).PadRight(25, ' '), temperature);
            Console.WriteLine("{0}Critical temperature is {1} C", " ".PadLeft(25, ' '), monitorDto.TemperatureAlert);

            Decimal criticalTemperature = monitorDto.TemperatureAlert ?? CRITICAL_TEMPERATURE_DEFAULT;
            return (temperature >= criticalTemperature) ? true : false;
        }

        private static void NotifyByMail(IoTTransaction iotTrsData)
        {
            HostRepository hostDal = new HostRepository();
            Host hostDto = hostDal.GetLast();

            if (hostDto == null) return;
            if (string.IsNullOrEmpty(hostDto.Protocal)) return;

            EMailStructure mail = ComposeMail(iotTrsData);
            EMailService emailService = new EMailService(hostDto.URL, hostDto.Port, hostDto.UserName, hostDto.Password);

            switch (hostDto.Protocal.Trim().ToUpper())
            {
                case "SMTP":
                    emailService.SendMailViaSMTP(mail);
                    break;

                case "EXCHAGEEWS":
                    emailService.SendMailViaExchangeEWS(mail);
                    break;
            }

        }

        private static EMailStructure ComposeMail(IoTTransaction iotTrsData)
        {
            MailRepository mailDal = new MailRepository();
            Mail mailDto = mailDal.GetLast();

            EMailStructure mail = new EMailStructure();

            mail.Sender = mailDto.Sender;
            mail.Recipients = mailDto.Recipients.Split(';');
            mail.Subject = mailDto.Subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = CreateRelayDataReportForMail(mailDto.Message, iotTrsData);
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHTML = true;

            return mail;
        }

        private static string CreateRelayDataReportForMail(string message, IoTTransaction iotTrsData)
        {
            message = message.Replace(SEARCH_LOG_TIME, iotTrsData.CurrentDateTime.ToString(REPORT_TIMESTAMP_FORMAT));

            message = message.Replace(string.Format(SEARCH_RELAY, 1), (iotTrsData.R1 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 2), (iotTrsData.R2 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 3), (iotTrsData.R3 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 4), (iotTrsData.R4 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 5), (iotTrsData.R5 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 6), (iotTrsData.R6 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 7), (iotTrsData.R7 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 8), (iotTrsData.R8 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 9), (iotTrsData.R9 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);
            message = message.Replace(string.Format(SEARCH_RELAY, 10), (iotTrsData.R10 == 1) ? RELAY_STATUS_ON : RELAY_STATUS_OFF);

            message = message.Replace(string.Format(SEARCH_SENSOR, 1), iotTrsData.T1.ToString());
            message = message.Replace(string.Format(SEARCH_SENSOR, 2), iotTrsData.T2.ToString());
            message = message.Replace(string.Format(SEARCH_SENSOR, 3), iotTrsData.T3.ToString());

            return message;
        }

	}
}
