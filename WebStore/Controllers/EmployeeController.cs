using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeeController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        [Breadcrumb("EmployeeList")]
        public IActionResult EmployeeList()
        {
            return View(_employeesData.GetAll());
        }

        [Breadcrumb("Employee details")]
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

        [Breadcrumb("Add-Edit")]
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            EmployeeView model;
            if (id.HasValue)
            {
                model = _employeesData.GetById(id.Value);
                if (ReferenceEquals(model, null))
                    return NotFound();
            }
            else
            {
                model = new EmployeeView();
            }

            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeView model)
        {
            if (model.Id > 0)
            {
                var dbItem = _employeesData.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                if (ModelState.IsValid)
                {
                    dbItem.FirstName = model.FirstName;
                    dbItem.SurName = model.SurName;
                    dbItem.Age = model.Age;
                    dbItem.Patronymic = model.Patronymic;
                    dbItem.Position = model.Position;
                }
            }
            else
            {
                if (ModelState.IsValid)
                    AddNew(model);
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(EmployeeList));
            else
                return View("Edit", model);
        }

        public void AddNew(EmployeeView model)
        {
            _employeesData.AddNew(model);
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _employeesData.Delete(id);
            return RedirectToAction(nameof(EmployeeList));
        }

    }

}