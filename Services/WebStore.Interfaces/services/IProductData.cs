using System.Collections.Generic;
using System.Linq;
using WebStore.Entities.Dto.Page;
using WebStore.Entities.Dto.Product;
using WebStore.Entities.Entities;
using WebStore.Entities.ViewModels;

namespace WebStore.Interfaces.services
{
    /// <summary>
    /// Интерфейс для работы с товарами
    /// </summary>
    public interface IProductData
    {
        /// <summary>
        /// Список секций
        /// </summary>
        /// <returns></returns>
        IEnumerable<Section> GetSections();

        /// <summary>
        /// Секция по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Section GetSectionById(int id);

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetBrands();

        /// <summary>
        /// Бренд по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Brand GetBrandById(int id);

        /// <summary>
        /// Список товаров с постраничным разбиением
        /// </summary>
        /// <param name="filter">Фильтр товаров</param>
        /// <returns></returns>
        PagedProductDto GetProducts(ProductFilter filter);

        /// <summary>
        /// Продукт
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашел, иначе null</returns>
        ProductDto GetProductById(int id);

        #region AdminFunctions

        /// <summary>
        /// AddNewProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        ProductViewModel Create(ProductViewModel product);

        /// <summary>
        /// EditProduct
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ProductEdit(ProductViewModel model);

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ProductDelete(int id);

        /// <summary>
        /// DetailsProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        #endregion


    }
}
