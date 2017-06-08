using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateEnd { get; set; }
        public virtual DutyJourney DutyJourney { get; set; }

    }
}