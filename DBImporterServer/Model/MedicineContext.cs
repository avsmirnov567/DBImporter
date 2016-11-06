namespace DBImporterServer.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MedicineContext : DbContext
    {
        public MedicineContext()
            : base("name=MedicineContext")
        {
        }

        public virtual DbSet<Diagnoses> Diagnoses { get; set; }
        public virtual DbSet<Diseases> Diseases { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Specs> Specs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diseases>()
                .HasMany(e => e.Diagnoses)
                .WithRequired(e => e.Diseases)
                .HasForeignKey(e => e.disease)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctors>()
                .HasMany(e => e.Diagnoses)
                .WithRequired(e => e.Doctors)
                .HasForeignKey(e => e.doctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patients>()
                .HasMany(e => e.Diagnoses)
                .WithRequired(e => e.Patients)
                .HasForeignKey(e => e.patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Specs>()
                .HasMany(e => e.Doctors)
                .WithRequired(e => e.Specs)
                .HasForeignKey(e => e.spec)
                .WillCascadeOnDelete(false);
        }
    }
}
