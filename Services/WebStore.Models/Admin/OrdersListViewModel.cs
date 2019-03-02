using System.Collections.Generic;

namespace WebStore.Models.Admin
{
    public class OrdersListViewModel
    {
        public IEnumerable<Entities.Entities.Order> OrdersList { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
