using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Horizoft.EMail
{
    public class EMailService
    {
        private int MAIL_SERVER_SMTP_TIMEOUT = 120000;
        private string host = string.Empty;
        private int port;
        private string userName = string.Empty;
        private string password = string.Empty;

        //public EMailService() { }

        public EMailService(string _host, int? _port, string _userName, string _password)
        {
            host = _host;
            port = _port ?? 25;
            userName = _userName;
            password = _password;
        }

        public void SendMailViaSMTP(EMailStructure email)
        {
            //string host = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_SMTP_HOST"];
            //string port = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_SMTP_PORT"];
            //string userName = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_SMTP_USERNAME"];
            //string password = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_SMTP_PASSWORD"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(userName)) return;

            //int port_result;

            //SmtpClient mailService = new SmtpClient(host, (int.TryParse(port, out port_result) ? port_result : 25));
            SmtpClient mailService = new SmtpClient(host, (port == 0) ? 25 : port);
            mailService.EnableSsl = true;
            mailService.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailService.UseDefaultCredentials = false;
            mailService.Timeout = MAIL_SERVER_SMTP_TIMEOUT; //milliseconds;
            mailService.Credentials = new System.Net.NetworkCredential(userName, password);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email.Sender);

            if (email.Recipients != null)
            {
                foreach (string recipient in email.Recipients)
                {
                    mail.To.Add(new MailAddress(recipient));
                }
            }

            if (email.CCRecipients != null)
            {
                foreach (string ccRecipient in email.CCRecipients)
                {
                    mail.CC.Add(new MailAddress(ccRecipient));
                }
            }

            mail.Subject = email.Subject;
            mail.SubjectEncoding = email.SubjectEncoding;
            mail.Body = email.Body;
            mail.BodyEncoding = email.BodyEncoding;
            mail.IsBodyHtml = email.IsBodyHTML;

            try
            {
                mailService.Send(mail);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void SendMailViaExchangeEWS(EMailStructure email)
        {
            //string host = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_EWS_HOST"];
            //string userName = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_EWS_USERNAME"];
            //string password = System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER_EWS_PASSWORD"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(userName)) return;

            ExchangeService mailService = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            if (!String.IsNullOrEmpty(host))
            {
                mailService.Url = new Uri(host);
            }
            else
            {
                //service.AutodiscoverUrl(senderMail, RedirectionUrlValidationCallback);
            }
            mailService.UseDefaultCredentials = false;
            mailService.Credentials = new WebCredentials(userName, password);
            mailService.TraceEnabled = true;

            EmailMessage mail = new EmailMessage(mailService);
            mail.From = new EmailAddress(email.Sender);
            mail.Sender = new EmailAddress(email.Sender);

            if (email.Recipients != null)
            {
                foreach (string recipient in email.Recipients)
                {
                    mail.ToRecipients.Add(new EmailAddress(recipient));
                }
            }

            if (email.CCRecipients != null)
            {
                foreach (string ccRecipient in email.CCRecipients)
                {
                    mail.CcRecipients.Add(new EmailAddress(ccRecipient));
                }
            }

            mail.Subject = email.Subject;
            mail.Body = email.Body;

            try
            {
                mail.Send();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
	}
}
