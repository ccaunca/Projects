﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CarloniusEntities : DbContext
    {
        public CarloniusEntities()
            : base("name=CarloniusEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Budget_Categories> Budget_Categories { get; set; }
        public virtual DbSet<Budget_Transactions> Budget_Transactions { get; set; }
    
        public virtual ObjectResult<Budget_GetAllCategories_Result> Budget_GetAllCategories()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Budget_GetAllCategories_Result>("Budget_GetAllCategories");
        }
    
        public virtual ObjectResult<GetTransactionsByDateTime_Result> GetTransactionsByDateTime(Nullable<System.DateTime> dateTime)
        {
            var dateTimeParameter = dateTime.HasValue ?
                new ObjectParameter("DateTime", dateTime) :
                new ObjectParameter("DateTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTransactionsByDateTime_Result>("GetTransactionsByDateTime", dateTimeParameter);
        }
    }
}