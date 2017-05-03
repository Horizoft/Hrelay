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
        public IHttpActionResult GetFirst()
        {
            try
            {
                Mail mail = mailRepository.GetFirst();
                //if (mail != null)
                {
                    return Ok(mail);
                }
                //else
                //{
                //    return StatusCode(HttpStatusCode.NotFound);
                //}
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpPost]
        public IHttpActionResult Add(Mail mail)
        {
            try
            {
                mailRepository.Add(mail);

                return Ok(mail);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpPost]
        public IHttpActionResult Update(Mail mail)
        {
            try
            {
                mailRepository.Update(mail);
                return Ok(mail);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        public IHttpActionResult Delete(Mail mail)
        {
            try
            {
                mailRepository.Delete(mail);
                return Ok(mail);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
