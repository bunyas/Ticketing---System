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
    
    public partial class View_Stage_Booking_Final
    {
        public Nullable<int> Schedule_ID { get; set; }
        public System.DateTime Departure_Time { get; set; }
        public Nullable<System.DateTime> Arrival_Time { get; set; }
        public Nullable<int> Seat_No { get; set; }
        public string Plate_No { get; set; }
        public string Route_Name { get; set; }
        public string Start_Stage { get; set; }
        public string Destination_Stage { get; set; }
        public Nullable<System.DateTime> Boarding_Time { get; set; }
        public Nullable<int> Booked_seats { get; set; }
        public Nullable<int> Remaining_Seats { get; set; }
        public string Booking_Status { get; set; }
    }
}