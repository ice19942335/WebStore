using System.Linq;
using WebStore.Entities.Entities;
using WebStore.Models.Admin;

namespace WebStore.Interfaces.services
{
    public interface IOrdersServiceAdmin
    {
        IQueryable<Order> GetAllOrdersList();
        OrderDetailsViewModel GetOrderById(int id);
        bool DeleteOdrerById(int id);
    }
}
