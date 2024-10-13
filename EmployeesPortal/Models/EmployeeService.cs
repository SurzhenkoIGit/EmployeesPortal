namespace EmployeesPortal.Models
{
    public class EmployeeService
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, FullName = "John Doe", Email = "john@example.com", Position = "Разработчик ПО", Department = Department.IT, HireDate = DateTime.Now.AddYears(-3), BirthDate = DateTime.Now.AddYears(-30), Type = EmployeeType.Постоянный, Gender = "Муж", Salary = 60000 },
            new Employee { Id = 2, FullName = "Jane Smith", Email = "jane@example.com", Position = "Менеджер по персоналу", Department = Department.HR, HireDate = DateTime.Now.AddYears(-5), BirthDate = DateTime.Now.AddYears(-35), Type = EmployeeType.Постоянный, Gender = "Жен", Salary = 80000 },
            new Employee { Id = 3, FullName = "Sam Wilson", Email = "sam@example.com", Position = "Менеджер по продажам", Department = Department.Sales, HireDate = DateTime.Now.AddYears(-2), BirthDate = DateTime.Now.AddYears(-28), Type = EmployeeType.Договор, Gender ="Муж", Salary = 50000 },
            new Employee { Id = 4, FullName = "Anna Taylor", Email = "anna@example.com", Position = "Исполнительный ассистент", Department = Department.Admin, HireDate = DateTime.Now.AddYears(-1), BirthDate = DateTime.Now.AddYears(-25), Type = EmployeeType.Временный, Gender = "Жен", Salary = 40000 },
            new Employee { Id = 5, FullName = "Tom Brown", Email = "tom@example.com", Position = "Сетевой инженер", Department = Department.IT, HireDate = DateTime.Now.AddYears(-4), BirthDate = DateTime.Now.AddYears(-32), Type = EmployeeType.Постоянный, Gender = "Муж", Salary = 70000 },
            new Employee { Id = 6, FullName = "Emma Davis", Email = "emma@example.com", Position = "Специалист по персоналу", Department = Department.HR, HireDate = DateTime.Now.AddYears(-6), BirthDate = DateTime.Now.AddYears(-34), Type = EmployeeType.Постоянный, Gender = "Жен", Salary = 75000 },
            new Employee { Id = 7, FullName = "Luke Miller", Email = "luke@example.com", Position = "Менеджер по продажам", Department = Department.Sales, HireDate = DateTime.Now.AddYears(-3), BirthDate = DateTime.Now.AddYears(-31), Type = EmployeeType.Договор, Gender = "Муж", Salary = 85000 },
            new Employee { Id = 8, FullName = "Olivia Johnson", Email = "olivia@example.com", Position = "Офис-менеджер", Department = Department.Admin, HireDate = DateTime.Now.AddYears(-2), BirthDate = DateTime.Now.AddYears(-29), Type = EmployeeType.Постоянный, Gender = "Жен", Salary = 65000 },
            new Employee { Id = 9, FullName = "Mia Moore", Email = "mia@example.com", Position = "Системный администратор", Department = Department.IT, HireDate = DateTime.Now.AddYears(-1), BirthDate = DateTime.Now.AddYears(-26), Type = EmployeeType.Стажер, Gender = "Жен", Salary = 30000 },
            new Employee { Id = 10, FullName = "Chris Evans", Email = "chris@example.com", Position = "Координатор по привлечению талантов", Department = Department.HR, HireDate = DateTime.Now.AddYears(-5), BirthDate = DateTime.Now.AddYears(-33), Type = EmployeeType.Временный, Gender = "Other", Salary = 55000 },
            new Employee { Id = 11, FullName = "Sophia White", Email = "sophia@example.com", Position = "Менеджер по продажам", Department = Department.Sales, HireDate = DateTime.Now.AddYears(-2), BirthDate = DateTime.Now.AddYears(-27), Type = EmployeeType.Постоянный, Gender = "Жен", Salary = 52000 },
            new Employee { Id = 12, FullName = "Liam Green", Email = "liam@example.com", Position = "Секретарь", Department = Department.Admin, HireDate = DateTime.Now.AddYears(-1), BirthDate = DateTime.Now.AddYears(-24), Type = EmployeeType.Временный, Gender = "Муж", Salary = 38000 },
            new Employee { Id = 13, FullName = "Noah Black", Email = "noah@example.com", Position = "Системный администратор", Department = Department.IT, HireDate = DateTime.Now.AddYears(-3), BirthDate = DateTime.Now.AddYears(-29), Type = EmployeeType.Постоянный, Gender = "Муж", Salary = 65000 },
            new Employee { Id = 14, FullName = "Isabella Blue", Email = "isabella@example.com", Position = "Специалист по персоналу", Department = Department.HR, HireDate = DateTime.Now.AddYears(-4), BirthDate = DateTime.Now.AddYears(-30), Type = EmployeeType.Постоянный, Gender = "Жен", Salary = 76000 },
            new Employee { Id = 15, FullName = "James Brown", Email = "james@example.com", Position = "Ответственный за работу с клиентами", Department = Department.Sales, HireDate = DateTime.Now.AddYears(-2), BirthDate = DateTime.Now.AddYears(-28), Type = EmployeeType.Договор, Gender = "Муж", Salary = 62000 }
        };

        public async Task<(List<Employee> Employees, int TotalCount)> GetEmployess(
            string SearchTerm,
            string SelectedDepartment,
            string SelectedType,
            int PageNumber,
            int PageSize)
        {
            var filterEmployees = _employees.AsQueryable();
            if(!string.IsNullOrEmpty(SearchTerm))
            {
                filterEmployees = filterEmployees.Where(p => p.FullName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(SelectedDepartment))
            {
                if (Enum.TryParse(SelectedDepartment, out Department department))
                {
                    filterEmployees = filterEmployees.Where(p => p.Department == department);
                }
            }

            if (!string.IsNullOrEmpty(SelectedType))
            {
                if (Enum.TryParse(SelectedType, out EmployeeType type))
                {
                    filterEmployees = filterEmployees.Where((p) => p.Type == type);
                }
            }

            int totalCount = filterEmployees.Count();

            filterEmployees = filterEmployees.Skip((PageNumber - 1) * PageSize).Take(PageSize);

            return await Task.FromResult((filterEmployees.ToList(), totalCount));
        }

        public Employee? GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void CreateEmployee(Employee employee)
        {
            employee.Id =_employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = GetEmployeeById(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.FullName = employee.FullName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Position = employee.Position;
                existingEmployee.Department = employee.Department;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.Type = employee.Type;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Salary = employee.Salary;
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
    }
}
