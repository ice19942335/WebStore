using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Entities.Entities;

namespace WebStore.Areas.Admin.Models
{
    public class ProductListViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
