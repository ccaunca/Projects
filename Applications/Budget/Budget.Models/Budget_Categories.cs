//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Budget.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Budget_Categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Budget_Categories()
        {
            this.Budget_Transactions = new HashSet<Budget_Transactions>();
        }
    
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Budget_Transactions> Budget_Transactions { get; set; }
    }
}
