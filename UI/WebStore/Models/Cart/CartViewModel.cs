using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Entities.Entities;

namespace WebStore.Models.Cart
{
    public class CartViewModel
    {
        public  Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        public int ItemsCoutn => Items?.Sum(e => e.Value) ?? 0;
    }
}
