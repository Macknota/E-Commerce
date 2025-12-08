using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Services.Specifications.Orders;
using Store.G02.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Orders
{
    public class OederService(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1. Get Order Address

            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);


            // 2. Get Delivery Method By Id

            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);


            // 3. Get Order Items
            // 3.1 Get Basket By Id (CustomerBasket By Id) from In-memroy DB So we need object from basket Repository
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            // 3.2 Convert Every Basket-Item To Order-Item

            var orderItems = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                // Check Price
                // Get Product From Db
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                // Important to check That the product is exist and his price is right for the Recipe
                if(product.Price != item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price,item.Quantity);
                orderItems.Add(orderItem);
            }


            // 4. Caculate SubTotal

            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);


            //Create Order In DB
            //using second ctor to intialize the values of the order
            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId);



            // Add Order In Database
            // We need To call Store DbContext from => Generic Repository => using Unit Of Work so we need object from unit of work 

            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrdertBadRequestException();

            return _mapper.Map<OrderResponse>(order);
            // return OrderResponse So i need to map it so i need object from imapper
            // And Mapping Profile needed to moidify
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail)
        {
            // first we will create Specification class for order
            // Second Call him
            var spec = new OrderSpecification(id, UserEmail);

            // We Have Navigational Properties in Order Table So We will use Specification Method
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);

            if (order is null) throw new OrderNotFoundException(id);

            return _mapper.Map<OrderResponse>(order);

        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmail)
        {
            var spec = new OrderSpecification(UserEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(order);

        }
    }
}
