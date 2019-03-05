using WebStore.Entities.ViewModels.Order;

namespace WebStore.Entities.ViewModels.Cart
{
    public class DetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }
    }
}
