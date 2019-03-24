using System.Collections.Generic;

namespace WebStore.Entities.Entities
{
    /// <summary>
    /// Класс для фильтрации товаров
    /// </summary>
    public class ProductFilter
    {
        /// <summary>
        /// Секция, к которой принадлежит товар
        /// </summary>
        public int? SectionId { get; set; }
        /// <summary>
        /// Бренд товара
        /// </summary>
        public int? BrandId { get; set; }
        /// <summary>
        /// Id товара
        /// </summary>
        public List<int> Ids { get; set; }
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int? PageSize { get; set; }
    }
}
