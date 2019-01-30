using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Implementations
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<EmployeeView> _employees;

        public InMemoryEmployeesData()
        {
            _employees = new List<EmployeeView>(3)
            {
                new EmployeeView(){Id = 1, FirstName = "Вася", SurName = "Пупкин", Patronymic = "Иванович", Age = 22, Position = "Директор"},
                new EmployeeView(){Id = 2, FirstName = "Иван", SurName = "Холявко", Patronymic = "Александрович", Age = 30, Position = "Программист"},
                new EmployeeView(){Id = 3, FirstName = "Роберт", SurName = "Серов", Patronymic = "Сигизмундович", Age = 50, Position = "Зав. склада"}
            };
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }

        public bool Commit(int id, string firstName = "null", string sureName = "null", string patronymic = "null", int age = -1)
        {
            //Получаем сотрудника по Id
            var employee = _employees.FirstOrDefault(t => t.Id.Equals(id));
            //Если такого не существует
            if (ReferenceEquals(employee, null))
                return false;

            if (firstName != "null" && sureName != "null" && patronymic != "null" && age != -1)
            {
                EmployeeView emp = _employees.ElementAt(_employees.FindIndex(e => e.Id == id));
                if (emp != null)
                {
                    emp.Id = id;
                    emp.FirstName = firstName;
                    emp.SurName = sureName;
                    emp.Patronymic = patronymic;
                    emp.Age = age;
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void AddNew(EmployeeView model)
        {
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
