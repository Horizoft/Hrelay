using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizoft.Relay.DTO;

namespace Horizoft.Relay.DAL
{
    public class MailRepository
    {
        private HorizoftRelayEntities entities = new HorizoftRelayEntities();

        public MailRepository(){}

        public Mail GetFirst()
        {
            Mail mail = new Mail();
            mail = entities.Mails.OrderByDescending(t => t.Id).FirstOrDefault();
            return mail;
        }

        public bool Add(Mail mail)
        {
            bool state = false;
            try
            {
                if (mail == null) return false;
                entities.Mails.Add(mail);
                entities.SaveChanges();
                state = true;
            }
            catch
            {
                state = false;
            }

            return state;
        }

        public bool Update(Mail mail)
        {
            bool state = false;
            try
            {
                if (mail == null) return false;

                var prop = mail.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var existingEntity = GetById(mail.Id);
                    if (existingEntity != null)
                    {
                        entities.Entry(existingEntity).CurrentValues.SetValues(mail);
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

        public bool Delete(Mail mail)
        {
            bool state = false;
            try
            {
                if (mail == null) return false;

                var existingEntity = GetById(mail.Id);
                if (existingEntity != null)
                {
                    entities.Mails.Remove(existingEntity);
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

        public Mail GetById(int id)
        {
            return entities.Mails.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
