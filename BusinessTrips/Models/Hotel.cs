using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Display(Name = "Название отеля: ")]
        public string Name { get; set; }
        [Display(Name = "Адрес отеля: ")]
        public string Address { get; set; }
        [Display(Name = "Город отеля: ")]
        public string City { get; set; }
        [Display(Name = "Дата прибытия: ")]
        public DateTime DateIn { get; set; }
        [Display(Name = "Название отправки: ")]
        public DateTime DateEnd { get; set; }

        public int DutyJourneyId { get; set; }
        public virtual DutyJourney DutyJourney { get; set; }

    }
}