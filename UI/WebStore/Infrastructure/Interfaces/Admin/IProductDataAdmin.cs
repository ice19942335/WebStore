using System.Linq;
using WebStore.Entities.Entities;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces.Admin
{
    public interface IProductDataAdmin
    {
        ProductViewModel Create(ProductViewModel product);

        bool ProductEdit(ProductViewModel model);

        bool ProductDelete(int id);

        ProductViewModel ProductDetails(int id);

        /// <summary>
        /// Get All Products from DB
        /// </summary>
        /// <returns>IQueryable Product</returns>
        IQueryable<Product> GetAllProducts();

        /// <summary>
        /// TEST method to fill list of products in Admin panel to save time
        /// </summary>
        /// <returns>Boolean</returns>
        bool FillListWithProductsDeleteLater();
    }
}
