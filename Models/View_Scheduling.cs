//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Buses.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_Scheduling
    {
        public int ID { get; set; }
        public int Bus_ID { get; set; }
        public string Plate_No { get; set; }
        public Nullable<int> Status_ID { get; set; }
        public string Status_Desc { get; set; }
        public System.DateTime Departure_Time { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public Nullable<System.DateTime> Arrival_Time { get; set; }
        public Nullable<System.DateTime> Boarding_Time { get; set; }
        public string Schedule_Desc { get; set; }
    }
}
