using WebStore.Entities.ViewModels.Cart;

namespace WebStore.Interfaces.services
{
    public interface ICartService
    {
        void DecrementFromCart(int id);

        void RemoveFromCart(int id);

        void RemoveAll();

        void AddToCart(int id);

        CartViewModel TransformCart();
    }
}
