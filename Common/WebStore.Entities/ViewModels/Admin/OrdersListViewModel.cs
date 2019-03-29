using System.Collections.Generic;
using WebStore.Entities.Dto.Order;

namespace WebStore.Entities.ViewModels.Admin
{
    public class OrdersListViewModel
    {
        public IEnumerable<OrderDto> OrdersList { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}
