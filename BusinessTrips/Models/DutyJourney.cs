using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.Models
{
    public class DutyJourney
    {
        /// <summary>
        /// Модель для работы с командировкой сотрудника
        /// </summary>
        public int Id { get; set; }
        public string WorkDay { get; set; }
        public string FreeDay { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Point { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public DutyJourney()
        {
            Employees = new List<Employee>();
        }
    }

}