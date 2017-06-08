using System.Collections.Generic;
using System.Data.Entity;

namespace BusinessTrips.Models
{
    public class BusinessTripsDbInitializer : DropCreateDatabaseIfModelChanges<BusinessTripsContext>
    {
        protected override void Seed(BusinessTripsContext context)
        {
            Employee empl1 = new Employee { Id = 1, Name = "Ivan", Surname = "Ivanovich", Lastname = "Ivanov" };
            DutyJourney dj1 = new DutyJourney { Id = 1, City = "Tomsk", Country = "Russia", Point = "Professioanl courses", FreeDay = "Monday", WorkDay = "Sunday", Employees = new List<Employee>() { empl1 } };

            context.Employees.Add(empl1);
            context.DutyJourneys.Add(dj1);
            base.Seed(context);
           // context.SaveChanges();
        }
    }
}