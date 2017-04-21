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
        public Monitor GetFirst()
        {
            return monitorRepository.GetFirst();
        }

        [HttpPost]
        public bool Add(Monitor monitor)
        {
           return monitorRepository.Add(monitor);
            
        }

        [HttpPost]
        public bool Update(Monitor monitor)
        {
            return monitorRepository.Update(monitor);
        }

        [HttpPost]
        public bool Delete(Monitor monitor)
        {
           return monitorRepository.Delete(monitor);
        }

    }
}
