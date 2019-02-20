﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Entities.Entities;
using WebStore.Models;
using WebStore.Models.Order;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersData
    {
        /// <summary>
        /// Список секций
        /// </summary>
        /// <returns></returns>
        IEnumerable<Section> GetSections();

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetBrands();

        /// <summary>
        /// Список заказов
        /// </summary>
        /// <param name="filter">Фильтр заказов</param>
        /// <returns></returns>
        IEnumerable<Order> GetOrders(OrderFilter filter);

        /// <summary>
        /// Продукт
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Order, если нашел, иначе null</returns>
        OrderViewModel GetOrderById(int id);
    }
}
