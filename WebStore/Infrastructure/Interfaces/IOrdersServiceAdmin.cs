using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Entities.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersServiceAdmin
    {
        IEnumerable<Order> GetAllOrdersList();
    }
}
