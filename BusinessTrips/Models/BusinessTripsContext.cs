using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;

namespace BusinessTrips.Models
{
    public class BusinessTripsContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Passage> Passeges { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DutyJourney> DutyJourneys { get; set; }
        
        public BusinessTripsContext() : base("BusTripConnection") { }
    }
}