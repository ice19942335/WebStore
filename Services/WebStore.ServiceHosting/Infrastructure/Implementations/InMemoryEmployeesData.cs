using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces.services;
using WebStore.Models;

namespace WebStore.ServiceHosting.Infrastructure.Implementations
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<EmployeeViewModel> _employees;
        public InMemoryEmployeesData()
        {
            _employees = new List<EmployeeViewModel>(3)
            {
                new EmployeeViewModel
                {
                    Id = 1, FirstName = "Вася", SurName ="Пупкин", Patronymic = "Иванович", Age = 22, Position = "Директор"
                },
                new EmployeeViewModel
                {
                    Id = 2, FirstName = "Иван", SurName ="Холявко", Patronymic = "Александрович", Age = 30, Position = "Программист"
                },
                new EmployeeViewModel
                {
                    Id = 3, FirstName = "Роберт", SurName = "Серов", Patronymic = "Сигизмундович", Age = 50, Position = "Зав. склада"
                }
            };
        }
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employees;
        }
        public EmployeeViewModel GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }
        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var employee = _employees.FirstOrDefault(e => e.Id.Equals(id));
            if (employee == null)
                throw new InvalidOperationException("Employee not exits");
            employee.Age = entity.Age;
            employee.FirstName = entity.FirstName;
            employee.Patronymic = entity.Patronymic;
            employee.SurName = entity.SurName;
            employee.Position = entity.Position;
            return employee;
        }
        public void Commit()
        {
            // Ничего не делаем
        }
        public void AddNew(EmployeeViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }
        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
    }
}
