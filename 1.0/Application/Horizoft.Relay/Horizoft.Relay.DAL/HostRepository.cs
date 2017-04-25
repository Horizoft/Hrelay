using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizoft.Relay.DTO;

namespace Horizoft.Relay.DAL
{
    public class HostRepository
    {
        private HorizoftRelayEntities entities = new HorizoftRelayEntities();

        public HostRepository() { }
        public Host GetFirst()
        {
                return entities.Hosts.OrderByDescending(t => t.Id).FirstOrDefault();
        }

        public Host Add(Host host)
        {
                entities.Hosts.Add(host);
                entities.SaveChanges();

            return host;
        }

        public Host Update(Host host)
        {
                var prop = host.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var existingEntity = GetById(host.Id);
                    if (existingEntity != null)
                    {
                        entities.Entry(existingEntity).CurrentValues.SetValues(host);
                        entities.SaveChanges();
                    }
                }

            return host;
        }

        public Host Delete(Host host)
        {
                var existingEntity = GetById(host.Id);
                if (existingEntity != null)
                {
                    entities.Hosts.Remove(existingEntity);
                    entities.SaveChanges();
                }

            return host;
        }

        public Host GetById(int id)
        {
            return entities.Hosts.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
