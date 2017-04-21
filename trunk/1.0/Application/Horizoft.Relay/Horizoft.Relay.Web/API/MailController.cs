using Horizoft.Relay.DTO;
using Horizoft.Relay.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Horizoft.Relay.API
{
    public class MailController : ApiController
    {
        private MailRepository mailRepository = new MailRepository();

        [HttpGet]
        public Mail GetFirst()
        {
            return mailRepository.GetFirst();
        }

        [HttpPost]
        public bool Add(Mail mail)
        {
            return mailRepository.Add(mail);

        }

        [HttpPost]
        public bool Update(Mail mail)
        {
            return mailRepository.Update(mail);
        }

        [HttpPost]
        public bool Delete(Mail mail)
        {
            return mailRepository.Delete(mail);
        }
    }
}
