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
    
    public partial class CustomerNOK
    {
        public int ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ContactType { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NIN_Number { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    
        public virtual A_Contact_Type A_Contact_Type { get; set; }
        public virtual A_Customer A_Customer { get; set; }
        public virtual A_Gender A_Gender { get; set; }
    }
}
