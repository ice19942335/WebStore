using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Entities.ViewModels;
using WebStore.Interfaces.services;

namespace WebStore.ServiceHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/employees")]
    public class EmployeesApiController : Controller, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData ?? throw new
                                 ArgumentNullException(nameof(employeesData));
        }
        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employeesData.GetAll();
        }
        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeViewModel GetById(int id)
        {
            return _employeesData.GetById(id);
        }
        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody]EmployeeViewModel model)
        {
            _employeesData.AddNew(model);
        }
        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeViewModel UpdateEmployee(int id, [FromBody]EmployeeViewModel
            entity)
        {
            return _employeesData.UpdateEmployee(id, entity);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesData.Delete(id);
        }
        [NonAction]
        public void Commit()
        {
        }
    }
}
