﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTOS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MTDBEntities : DbContext
    {
        public MTDBEntities()
            : base("name=MTDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<COMPANY_BASIC_INFO> COMPANY_BASIC_INFO { get; set; }
        public virtual DbSet<PRODUCT_DOCUMENT> PRODUCT_DOCUMENT { get; set; }
        public virtual DbSet<CONTACT_US> CONTACT_US { get; set; }
        public virtual DbSet<PRODUCT> PRODUCT { get; set; }
        public virtual DbSet<SERIES> SERIES { get; set; }
    }
}
