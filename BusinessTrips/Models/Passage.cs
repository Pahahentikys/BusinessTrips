using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class Passage
    {
        public int Id { get; set; }
        [Display(Name ="Дата отъезда: ")]
        public DateTime? DateofDeparture { get; set; }
        [Display(Name = "Дата приезда: ")]
        public DateTime? DateofArrival { get; set; }
        [Display(Name = "Транспорт: ")]
        public string Transport { get; set; }
        [Display(Name = "Место назначения: ")]
        public string DestinationPoint { get; set; }
        [Display(Name = "Место прибытия: ")]
        public string DeparturePoint { get; set; }
  
        public int DutyJourneyId { get; set; }
        public virtual DutyJourney DutyJourney { get; set; }

    }
}