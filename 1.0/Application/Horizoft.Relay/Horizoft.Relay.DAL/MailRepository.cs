using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Horizoft.Relay.DTO;


namespace Horizoft.Relay.DAL
{
    public class MailRepository 
    {
        private HorizoftRelayEntities entities = new HorizoftRelayEntities();

        public MailRepository(){}

        public Mail GetFirst()
        {
            return entities.Mails.OrderByDescending(t => t.Id).FirstOrDefault();
        }

        public Mail Add(Mail mail)
        {
                entities.Mails.Add(mail);
                entities.SaveChanges();
        
            return mail;
        }

        public Mail Update(Mail mail)
        {
              var prop = mail.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var existingEntity = GetById(mail.Id);
                    if (existingEntity != null)
                    {
                        entities.Entry(existingEntity).CurrentValues.SetValues(mail);
                        entities.SaveChanges();
                       
                    }
                }
      
                return mail;
        }

        public Mail Delete(Mail mail)
        {
                var existingEntity = GetById(mail.Id);
                if (existingEntity != null)
                {
                    entities.Mails.Remove(existingEntity);
                    entities.SaveChanges();
                }
        
            return mail;
        }

        //public Mail GetFirst()
        //{
        //    Mail mail = new Mail();
        //    try
        //    {
        //        mail = entities.Mails.OrderByDescending(t => t.Id).FirstOrDefault();
        //    }
        //    catch
        //    {
        //        mail = null;
        //    }

        //    return mail;
        //}

        //public bool Add(Mail mail)
        //{
        //    bool state = false;
        //    try
        //    {
        //        if (mail == null) return false;
        //        entities.Mails.Add(mail);
        //        entities.SaveChanges();
        //        state = true;
        //    }
        //    catch
        //    {
        //        state = false;
        //    }

        //    return state;
        //}

        //public bool Update(Mail mail)
        //{
        //    bool state = false;
        //    try
        //    {
        //        if (mail == null) return false;

        //        var prop = mail.GetType().GetProperty("Id");
        //        if (prop != null)
        //        {
        //            var existingEntity = GetById(mail.Id);
        //            if (existingEntity != null)
        //            {
        //                entities.Entry(existingEntity).CurrentValues.SetValues(mail);
        //                entities.SaveChanges();
        //                state = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        state = false;
        //    }

        //    return state;
        //}

        //public bool Delete(Mail mail)
        //{
        //    bool state = false;
        //    try
        //    {
        //        if (mail == null) return false;

        //        var existingEntity = GetById(mail.Id);
        //        if (existingEntity != null)
        //        {
        //            entities.Mails.Remove(existingEntity);
        //            entities.SaveChanges();
        //            state = true;
        //        }
        //    }
        //    catch
        //    {
        //        state = false;
        //    }

        //    return state;
        //}

        public Mail GetById(int id)
        {
            return entities.Mails.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
