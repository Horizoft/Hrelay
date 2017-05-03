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
    public class MonitorController : ApiController
    {
        private MonitorRepository monitorRepository = new MonitorRepository();
       
        [HttpGet]
        public IHttpActionResult GetFirst()
        {
            try
            {
                Monitor monitor = monitorRepository.GetFirst();
                //if (monitor != null)
                {
                    return Ok(monitor);
                }
                //else
                //{
                //    return StatusCode(HttpStatusCode.NotFound);
                //}
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Add(Monitor monitor)
        {
            try
            {
                monitorRepository.Add(monitor);

                return Ok(monitor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        public IHttpActionResult Update(Monitor monitor)
        {
            try
            {
                monitorRepository.Update(monitor);
                return Ok(monitor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(Monitor monitor)
        {
            try
            {
                monitorRepository.Delete(monitor);
                return Ok(monitor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
