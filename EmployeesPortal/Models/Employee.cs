using System.ComponentModel.DataAnnotations;

namespace EmployeesPortal.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Неверное имя!")]
        [StringLength(100, ErrorMessage = "Полное имя не должно содержать более 100 символов")]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Адрес почты неверный!")]
        [EmailAddress(ErrorMessage = "Проверьте адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ошибка в наименовании должности!")]
        [StringLength(50, ErrorMessage = "В наименовании должности не должно быть больше 50 символов")]
        [Display(Name = "Должность")]
        public string Position {  get; set; }

        [Required(ErrorMessage = "Ошибка в наименовании подразделения!")]
        [Display(Name = "Подразделение")]
        public Department? Department { get; set; }

        [Required(ErrorMessage = "Неверная дата приема на работу!")]
        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты!")]
        public DateTime? HireDate { get; set; }

        [Required(ErrorMessage = "Неверная дата рождения!")]
        [Display(Name = "Дата рождрения")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты!")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Ошибка вида занятости!")]
        [Display(Name = "Тип занятости")]
        public EmployeeType? Type { get; set; }

        [Required(ErrorMessage = "Не указан пол!")]
        [StringLength(3, ErrorMessage = "Укажите МУЖ или ЖЕН")]
        [Display(Name = "Пол")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Ошибка суммы заработка!")]
        [Range(0, double.MaxValue, ErrorMessage = "Зарплата не может быть отрицательной")]
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }

    }
}
