using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string OfficialPosition { get; set; }
        public string Pasport { get; set; }
        public string NumberPhone { get; set; }

        public virtual ICollection<DutyJourney> DutyJourneys { get; set; }
        public Employee()
        {
            DutyJourneys = new List<DutyJourney>();
        }
    }
}