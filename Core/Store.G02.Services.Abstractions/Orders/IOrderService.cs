using Store.G02.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Orders
{
    public interface IOrderService
    {
        // 3 => Busket Id , Address , Delivery Method Id from(Object In Request Body)
        //Email From TOKEN
        Task<OrderResponse?> CreateOrderAsync(OrderRequest orderRequest,string userEmail);
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail);
        Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmail);
    }
}
