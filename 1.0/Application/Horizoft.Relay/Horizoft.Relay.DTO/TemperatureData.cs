//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TemperatureData
    {
        public long Id { get; set; }
        public System.DateTime MonitoredDateTime { get; set; }
        public System.DateTime MonitoredDate { get; set; }
        public System.TimeSpan MonitoredTime { get; set; }
        public Nullable<int> PlaceId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> I1 { get; set; }
        public Nullable<int> I2 { get; set; }
        public Nullable<int> R1 { get; set; }
        public Nullable<int> R2 { get; set; }
        public Nullable<int> R3 { get; set; }
        public Nullable<int> R4 { get; set; }
        public Nullable<int> R5 { get; set; }
        public Nullable<int> R6 { get; set; }
        public Nullable<int> R7 { get; set; }
        public Nullable<int> R8 { get; set; }
        public Nullable<int> R9 { get; set; }
        public Nullable<int> R10 { get; set; }
        public Nullable<decimal> S1 { get; set; }
        public Nullable<decimal> S2 { get; set; }
        public Nullable<decimal> S3 { get; set; }
        public string Source { get; set; }
    }
}
