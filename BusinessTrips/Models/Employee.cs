using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessTrips.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните данное поле!")]
        [StringLength(25, ErrorMessage = "Максимальное количество символов {0} не может быть меньше {2}.", MinimumLength = 2)]
        [Display(Name = "Имя сотрудника: ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните данное поле!")]
        [StringLength(25, ErrorMessage = "Максимальное количество символов {0} не может быть меньше {2}.", MinimumLength = 2)]
        [Display(Name = "Фамилия сотрудника: ")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Заполните данное поле!")]
        [StringLength(25, ErrorMessage = "Максимальное количество символов {0} не может быть меньше {2}.", MinimumLength = 6)]
        [Display(Name = "Отчество сотрудника: ")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Заполните данное поле!")]
        [Display(Name = "Дата рождения сотрудника: ")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage ="Заполните данное поле!")]
        [Display(Name = "Должность сотрудника: ")]
        public string OfficialPosition { get; set; }
        [Display(Name = "Номер паспорта: ")]
        [RegularExpression(@"\d{6,6}", ErrorMessage = "Номер паспорта должен состоять из 6 цифр.")]
        public string Pasport { get; set; }
        [Display(Name = "Телефонный номер: ")]
        [RegularExpression(@"^(8|\+7)\([0-9]{3}\)[0-9]{3}-[0-9]{2}-[0-9]{2}", ErrorMessage = "Номер по должен быть по формату: 8(923)178-93-28 или +7(923)178-93-28")]
        public string NumberPhone { get; set; }

        public int DutyJourneyId { get; set; }
        public virtual DutyJourney DutyJourneys { get; set; }
        public string City { get; internal set; }
        public string Transport { get; internal set; }
    }
}