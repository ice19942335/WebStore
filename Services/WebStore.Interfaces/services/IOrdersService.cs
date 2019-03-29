using System.Collections.Generic;
using System.Linq;
using WebStore.Entities.Dto.Order;
using WebStore.Entities.Entities;
using WebStore.Entities.ViewModels.Admin;

namespace WebStore.Interfaces.services
{
    public interface IOrdersService
    {
        /// <summary>
        /// Returning user orders
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>IEnumerable OrderDto</returns>
        IEnumerable<OrderDto> GetUserOrders(string userName);

        /// <summary>
        /// Returning order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderDto</returns>
        OrderDto GetOrderById(int id);

        /// <summary>
        /// Creating new order
        /// </summary>
        /// <param name="orderModel"></param>
        /// <param name="userName"></param>
        /// <returns>OrderDto</returns>
        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);

        #region AdminFunctions

        /// <summary>
        /// Returning IQueryable Orders
        /// </summary>
        /// <returns>IQueryable Orders</returns>
        List<OrderDto> GetAllOrdersList();

        /// <summary>
        /// Deleting order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean</returns>
        bool DeleteOdrerById(int id);

        #endregion
    }
}
