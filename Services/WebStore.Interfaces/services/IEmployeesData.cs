using System.Collections.Generic;
using WebStore.Entities.ViewModels;

namespace WebStore.Interfaces.services
{
    /// <summary>
    /// Интерфейс для работы с сотрудниками
    /// </summary>
    public interface IEmployeesData
    {
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeViewModel> GetAll();
        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        EmployeeViewModel GetById(int id);
        /// <summary>
        /// Обновление сотрудника
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="entity">Сотрудник для обновления</param>
        /// <returns></returns>
        EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel entity);
        /// <summary>
        /// Добавить нового
        /// </summary>
        /// <param name="model"></param>
        void AddNew(EmployeeViewModel model);
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        void Commit();
    }
}
