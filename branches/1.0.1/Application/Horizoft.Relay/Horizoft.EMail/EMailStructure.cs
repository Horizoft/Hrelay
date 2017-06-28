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
    public class EMailStructure
    {
        public string From { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string[] CCRecipients { get; set; }
        public string Subject { get; set; }
        public Encoding SubjectEncoding { get; set; }
        public string Body { get; set; }
        public Encoding BodyEncoding { get; set; }
        public bool IsBodyHTML { set; get; }

    }

}
