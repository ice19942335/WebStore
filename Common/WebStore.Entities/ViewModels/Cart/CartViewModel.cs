using System.Collections.Generic;
using System.Linq;

namespace WebStore.Entities.ViewModels.Cart
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        public int ItemsCoutn => Items?.Sum(e => e.Value) ?? 0;
    }
}
