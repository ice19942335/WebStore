using System.Collections.Generic;
using System.Linq;
using WebStore.Areas.Admin.Models;
using WebStore.Entities.Entities;

namespace WebStore.Infrastructure.Interfaces.Admin
{
    public interface IOrdersServiceAdmin
    {
        IQueryable<Order> GetAllOrdersList();
        OrderDetailsViewModel GetOrderById(int id);
        bool DeleteOdrerById(int id);
    }
}
