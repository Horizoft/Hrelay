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
            Host host = new Host();
            try
            {
                host = entities.Hosts.OrderByDescending(t => t.Id).FirstOrDefault();
            }
            catch
            {
                host = null;
            }
            return host;
        }

        public bool Add(Host host)
        {
            bool state = false;
            try
            {
                if (host == null) return false;
                entities.Hosts.Add(host);
                entities.SaveChanges();
                state = true;
            }
            catch
            {
                state = false;
            }

            return state;
        }

        public bool Update(Host host)
        {
            bool state = false;
            try
            {
                if (host == null) return false;

                var prop = host.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var existingEntity = GetById(host.Id);
                    if (existingEntity != null)
                    {
                        entities.Entry(existingEntity).CurrentValues.SetValues(host);
                        entities.SaveChanges();
                        state = true;
                    }
                }
            }
            catch
            {
                state = false;
            }

            return state;
        }

        public bool Delete(Host host)
        {
            bool state = false;
            try
            {
                if (host == null) return false;

                var existingEntity = GetById(host.Id);
                if (existingEntity != null)
                {
                    entities.Hosts.Remove(existingEntity);
                    entities.SaveChanges();
                    state = true;
                }
            }
            catch
            {
                state = false;
            }

            return state;
        }

        public Host GetById(int id)
        {
            return entities.Hosts.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
