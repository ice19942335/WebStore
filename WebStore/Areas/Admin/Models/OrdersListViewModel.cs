using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Entities.Entities;

namespace WebStore.Areas.Admin.Models
{
    public class OrdersListViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Order> OrdersList { get; set; }
    }
}
