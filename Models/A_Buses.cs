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
    
    public partial class A_Buses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public A_Buses()
        {
            this.Bus_Scheduling = new HashSet<Bus_Scheduling>();
        }
    
        public int Bus_ID { get; set; }
        public Nullable<int> Company { get; set; }
        public Nullable<int> Driver_ID { get; set; }
        public Nullable<int> Manufacturer { get; set; }
        public Nullable<int> Route_ID { get; set; }
        public string Model { get; set; }
        public string Plate_No { get; set; }
        public Nullable<int> Seat_No { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> EditionDate { get; set; }
    
        public virtual A_Companies A_Companies { get; set; }
        public virtual A_Employees A_Employees { get; set; }
        public virtual A_Manufacturer A_Manufacturer { get; set; }
        public virtual A_Routes A_Routes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bus_Scheduling> Bus_Scheduling { get; set; }
    }
}
