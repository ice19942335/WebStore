using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Entities.Entities;

namespace WebStore.Areas.Admin.Models
{
    public class OrdersListViewModel
    {
        public IEnumerable<Order> OrdersList { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
