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
    
    public partial class Customer_Booking
    {
        public string Booking_ID { get; set; }
        public Nullable<int> Schedule_ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public string Customer_Location { get; set; }
        public Nullable<bool> Customer_Comfirmed { get; set; }
    
        public virtual A_Customer A_Customer { get; set; }
        public virtual Bus_Scheduling Bus_Scheduling { get; set; }
    }
}
