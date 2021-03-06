﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Horizoft.Relay.DTO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HorizoftRelayEntities : DbContext
    {
        public HorizoftRelayEntities()
            : base("name=HorizoftRelayEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TemperatureData> TemperatureDatas { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<Mail> Mails { get; set; }
        public virtual DbSet<Monitor> Monitors { get; set; }
    
        public virtual ObjectResult<SPTemperatureReport_Result> SPTemperatureReport(Nullable<int> placeId, Nullable<int> areaId, Nullable<System.DateTime> date)
        {
            var placeIdParameter = placeId.HasValue ?
                new ObjectParameter("placeId", placeId) :
                new ObjectParameter("placeId", typeof(int));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("areaId", areaId) :
                new ObjectParameter("areaId", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTemperatureReport_Result>("SPTemperatureReport", placeIdParameter, areaIdParameter, dateParameter);
        }
    }
}
