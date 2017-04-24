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

        public MonitorRepository()
        {
        
        }
        public Monitor GetFirst()
        {
            Monitor monitor = new Monitor();
            try
            {
                monitor = entities.Monitors.OrderByDescending(t => t.Id).FirstOrDefault();
            }
            catch
            {
                monitor = null;
            }
            
            return monitor;
        }

        public bool Add(Monitor monitor)
        {
            bool state = false;
            try
            {
                if (monitor == null) return false;
                entities.Monitors.Add(monitor);
                entities.SaveChanges();
                state = true;
            }
            catch
            {
                state = false;
            }

            return state;
        }
        public bool Update(Monitor monitor)
        {
            bool state = false;
            try
            {             
                if (monitor == null) return false;

                var prop = monitor.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var existingEntity = GetById(monitor.Id);
                    if (existingEntity != null)
                    {
                        entities.Entry(existingEntity).CurrentValues.SetValues(monitor);
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
        //public int Update(Monitor monitor)
        //{

        //    if (monitor == null) return -1;

        //    var prop = monitor.GetType().GetProperty("Id");
        //    if (prop != null)
        //    {
        //        var existingEntity = GetById(monitor.Id);
        //        if (existingEntity != null)
        //        {
        //            entities.Entry(existingEntity).CurrentValues.SetValues(monitor);
        //        }
        //    }

        //   return entities.SaveChanges();
        //}

        public bool Delete(Monitor monitor)
        {
            bool state = false;
            try
            {
                if (monitor == null) return false;

                var existingEntity = GetById(monitor.Id);
                if (existingEntity != null)
                {
                    entities.Monitors.Remove(existingEntity);
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

        public Monitor GetById(int id)
        {
            return entities.Monitors.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
