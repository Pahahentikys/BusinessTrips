using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;

namespace BusinessTrips.Models
{
    public partial class BusinessTripsContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Passage> Passeges { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DutyJourney> DutyJourneys { get; set; }

        public BusinessTripsContext() : base("BusTripConnection") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DutyJourney>()
        //      .HasMany(p => p.Passages) 
        //      .WithRequired(p => p.DutyJourney)  
        //      .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<DutyJourney>()
        //        .HasMany(h => h.Hotels)
        //        .WithRequired(h => h.DutyJourney)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Employee>()
        //        .HasMany(dj => dj.DutyJourneys)
        //        .WithRequired(dj => dj.Employees)
        //        .WillCascadeOnDelete(true);
                
                

         
        //    //modelBuilder.Conventions.Remove<>
        //    //modelBuilder.Entity<Employee>()
        //    //.HasMany(dj => dj.DutyJourneys)
        //    //.WithMany(e => e.Employees);
        //    //.WillCascadeOnDelete(true);
        //}

    }
}