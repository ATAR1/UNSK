﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAndTunes
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JournalDBEntities : DbContext
    {
        public JournalDBEntities()
            : base("name=JournalDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<JournalRecord> JournalRecords { get; set; }
        public virtual DbSet<Employee> Personal { get; set; }
        public virtual DbSet<Shift> ShiftSet { get; set; }
        public virtual DbSet<Defectoscope> Defectoscope { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<WorkArea> WorkAreas { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Normative> Normatives { get; set; }
    }
}
