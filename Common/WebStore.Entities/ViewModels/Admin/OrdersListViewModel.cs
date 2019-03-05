using System.Collections.Generic;

namespace WebStore.Entities.ViewModels.Admin
{
    public class OrdersListViewModel
    {
        public IEnumerable<Entities.Order> OrdersList { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
