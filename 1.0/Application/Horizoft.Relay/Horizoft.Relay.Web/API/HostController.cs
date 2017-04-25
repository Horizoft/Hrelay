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
    public class HostController : ApiController
    {
        private HostRepository hostRepository = new HostRepository();

        [HttpGet]
        public IHttpActionResult GetFirst()
        {
            try
            {
                Host host = hostRepository.GetFirst();
                if (host != null)
                {
                    return Ok(host);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Add(Host host)
        {
            try
            {
                hostRepository.Add(host);

                return Ok(host);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Update(Host host)
        {
            try
            {
                hostRepository.Update(host);
                return Ok(host);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(Host host)
        {
            try
            {
                hostRepository.Delete(host);
                return Ok(host);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
