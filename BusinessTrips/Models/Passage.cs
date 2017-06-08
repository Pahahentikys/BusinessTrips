using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class Passage
    {
        public int Id { get; set; }
        public DateTime DateofDeparture { get; set; }
        public DateTime DateofArrival { get; set; }
        public string Transport { get; set; }
        public string DestinationPoint { get; set; }
        public string DeparturePoint { get; set; }
        public virtual DutyJourney DutyJourney { get; set; }
        


    }
}