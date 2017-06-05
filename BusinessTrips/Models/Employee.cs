using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class Employee
    {
        int Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Lastname { get; set; }

        public virtual ICollection<DutyJourney> DutyJourneys { get; set; }
        public Employee()
        {
            DutyJourneys = new List<DutyJourney>();
        }
    }
}