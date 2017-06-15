using System.Collections.Generic;
using System.Data.Entity;

namespace BusinessTrips.Models
{
    public class BusinessTripsDbInitializer : DropCreateDatabaseIfModelChanges<BusinessTripsContext>
    //public class BusinessTripsDbInitializer : DropCreateDatabaseAlways<BusinessTripsContext>
    {
        protected override void Seed(BusinessTripsContext context)
        {
            Passage psg1 = new Passage
            {
                Id = 1,
                DateofDeparture = new System.DateTime(2017, 10, 10, 9, 00, 00),
                DateofArrival = new System.DateTime(2017, 10, 12, 12, 10, 00),
                Transport = "avto",
                DestinationPoint = "Kemerovo",
                DeparturePoint = "Tomsk"
            };
            Hotel h1 = new Hotel
            {
                Id = 1,
                City = "Seversk",
                Address = "st. Kolotyshkina",
                DateIn = System.DateTime.Now,
                DateEnd = new System.DateTime(2017, 10, 6, 20, 30, 10),
                Name = "Work seminar"
            };
            Employee empl1 = new Employee
            {
                Id = 1,
                Name = "Ivan",
                Surname = "Ivanovich",
                Lastname = "Ivanov",
                BirthDate = new System.DateTime(2000, 10, 10),
                OfficialPosition = "engeener",
                Pasport = "323132",
                NumberPhone = "+7(986)542-14-87",
            };
            DutyJourney dj1 = new DutyJourney
            {
                Id = 1,
                City = "Tomsk",
                Country = "Russia",
                Point = "Professioanl courses",
                FreeDay = "Monday",
                WorkDay = "Sunday",
                Additionally = "доп деньги",
                BeginTrip = new System.DateTime(2017, 12, 12),
                FinalTrip = new System.DateTime(2017, 12, 07),
                Employees = new List<Employee>() { empl1},
                Hotels = new List<Hotel>() { h1 },
                Passages = new List<Passage>() { psg1 },
            };



            context.Hotels.Add(h1);
            context.Passeges.Add(psg1);
            context.Employees.Add(empl1);
            context.DutyJourneys.Add(dj1);
            base.Seed(context);
            context.SaveChanges();
        }
    }
}