using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.Models
{

    public class DutyJourney
    {
        /// <summary>
        /// Модель для работы с командировкой сотрудника
        /// </summary>
        /// 
        public int Id { get; set; }
        [Display(Name = "Цель коммандировки: ")]
        public string Point { get; set; }
        [Display(Name = "Рабочие дни: ")]
        public string WorkDay { get; set; }
        [Display(Name = "Выходные дни: ")]
        public string FreeDay { get; set; }
        [Display(Name = "Страна: ")]
        public string Country { get; set; }
        [Display(Name = "Город командировки: ")]
        public string City { get; set; }
        [Display(Name = "Дополнительно: ")]
        public string Additionally { get; set; }
        [Display(Name = "Начало командировки: ")]
        public DateTime BeginTrip { get; set; }
        [Display(Name = "Конец командировки: ")]
        public DateTime FinalTrip { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Passage> Passages { get; set; }

        public DutyJourney()
        {
            Employees = new List<Employee>();
            Passages = new List<Passage>();
            Hotels = new List<Hotel>();
        }
    }

}