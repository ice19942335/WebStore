using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeeController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        public IActionResult EmployeeList()
        {
            return View(_employeesData.GetAll());
        }

        public IActionResult Details(int id)
        {
            // Получаем сотрудника по Id
            var employee = _employeesData.GetById(id);

            // Если такого не существует
            if (ReferenceEquals(employee, null))
                return NotFound(); // возвращаем результат 404 Not Found

            // Иначе возвращаем сотрудника
            return View(employee);
        }

        public IActionResult Commit(int id, string firstName = "null", string sureName = "null", string patronymic = "null", int age = -1)
        {
            if (_employeesData.Commit(id, firstName, sureName, patronymic, age))
                return View("Details", _employeesData.GetById(id));
            else
                return NotFound();
        }
    }
}