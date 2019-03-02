using System.Linq;
using WebStore.Entities.Entities;
using WebStore.Models;
using WebStore.Models.Admin;

namespace WebStore.Interfaces
{
    public interface IOrdersServiceAdmin
    {
        IQueryable<Order> GetAllOrdersList();
        OrderDetailsViewModel GetOrderById(int id);
        bool DeleteOdrerById(int id);
    }
}
