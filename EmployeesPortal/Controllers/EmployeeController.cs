using EmployeesPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeesPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] string SearchTerm,
            [FromQuery] string SelectedDepartment,
            [FromQuery] string SelectedType,
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 5)
        {
            var (employees, totalCount) = await _employeeService.GetEmployess(SearchTerm, SelectedDepartment, SelectedType, PageNumber, PageSize);
            var viewModel = new EmployeeListViewModel
            {
                Employees = employees,
                PageNumber = PageNumber,
                PageSize = PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize),
                SearchTerm = SearchTerm,
                SelectedDepartment = SelectedDepartment,
                SelectedType = SelectedType
            };

            GetSelectLists();
            ViewBag.PageSizeOptions = new SelectList(new List<int> { 3, 5, 10, 15, 20, 25 }, PageSize);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            GetSelectLists();
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.CreateEmployee(employee);
                return RedirectToPage("Success!", new { id = employee.Id });
            }
            GetSelectLists();
            return View(employee);
        }

        public IActionResult Success([FromRoute] int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();

            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Update([FromRoute] int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(employee);
                TempData["Сообщение"] = $"Сотрудник под номером {employee.Id} и с именем {employee.FullName} успешно обновлен!";
                return RedirectToAction("List");
            }
            GetSelectLists();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            TempData["Сообщение"] = $"Сотрудник под номером {employee.Id} и с именем {employee.FullName} успешно удален!";
            return RedirectToAction("List");
        }

        [HttpGet]
        public JsonResult GetPositions(string department)
        {
            /*var Positions = new Dictionary<Department, List<string>>
            {
                { Department.IT, new List<string> { "Разработка ПО", "Системное администрирование", "Сетевое администрирование" } },
                { Department.HR, new List<string> { "Специалист по кадрам", "Менджер по кадрам", "Координатор" } },
                { Department.Sales, new List<string> { "Менеджер продаж", "Специалист по продажам", "Начальник отдела" } },
                { Department.Admin, new List<string> { "Офис-менеджер", "Ассистент", "Служащий ресепшена" } }
            };*/

            var Positions = new Dictionary<string, List<string>>
            {
                { "IT", new List<string> { "Разработка ПО", "Системное администрирование", "Сетевое администрирование" } },
                { "HR", new List<string> { "Специалист по кадрам", "Менджер по кадрам", "Координатор" } },
                { "Sales", new List<string> { "Менеджер продаж", "Специалист по продажам", "Начальник отдела" } },
                { "Admin", new List<string> { "Офис-менеджер", "Ассистент", "Служащий ресепшена" } }
            };
            var result = Positions.ContainsKey(department) ? Positions[department] : new List<string>();
            return Json(result);
        }

        private void GetSelectLists()
        {
            ViewBag.DepartmentOptions = new SelectList(Enum.GetValues(typeof(Department)).Cast<Department>());
            ViewBag.EmployeeTypeOptions = new SelectList(Enum.GetValues<EmployeeType>().Cast<EmployeeType>());
        }
    }

}
