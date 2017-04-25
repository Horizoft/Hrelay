using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizoft.Relay.DTO;
using System.Data.Entity;

namespace Horizoft.Relay.DAL
{
    public class MonitorRepository
    {
        private HorizoftRelayEntities entities = new HorizoftRelayEntities();

        public MonitorRepository(){}
        public Monitor GetFirst()
        {
            return entities.Monitors.OrderByDescending(t => t.Id).FirstOrDefault();
        }

        public Monitor Add(Monitor monitor)
        {
            entities.Monitors.Add(monitor);
            entities.SaveChanges();
            
            return monitor;
        }
        public Monitor Update(Monitor monitor)
        {       
                var prop = monitor.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var existingEntity = GetById(monitor.Id);
                    if (existingEntity != null)
                    {
                        entities.Entry(existingEntity).CurrentValues.SetValues(monitor);
                        entities.SaveChanges();
                       
                    }
                }
           
            return monitor;
        }

        public Monitor Delete(Monitor monitor)
        {
                var existingEntity = GetById(monitor.Id);
                if (existingEntity != null)
                {
                    entities.Monitors.Remove(existingEntity);
                    entities.SaveChanges();
                }

            return monitor;
        }

        public Monitor GetById(int id)
        {
            return entities.Monitors.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
