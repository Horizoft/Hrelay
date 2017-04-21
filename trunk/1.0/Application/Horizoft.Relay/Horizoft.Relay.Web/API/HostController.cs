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
        public Host GetFirst()
        {
            return hostRepository.GetFirst();
        }

        [HttpPost]
        public bool Add(Host host)
        {
            return hostRepository.Add(host);

        }

        [HttpPost]
        public bool Update(Host host)
        {
            return hostRepository.Update(host);
        }

        [HttpPost]
        public bool Delete(Host host)
        {
            return hostRepository.Delete(host);
        }
    }
}
