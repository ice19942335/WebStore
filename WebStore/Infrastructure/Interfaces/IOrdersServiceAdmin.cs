using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Areas.Admin.Models;
using WebStore.Entities.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersServiceAdmin
    {
        IEnumerable<Order> GetAllOrdersList();
        OrderDetailsViewModel GetOrderById(int id);
    }
}
