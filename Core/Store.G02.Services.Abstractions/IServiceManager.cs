using Store.G02.Services.Abstractions.Auth;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Services.Abstractions.Cache;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Services.Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IServiceManager
    {
        //Make a Property for each service
        IProductService ProductService { get; } //ReadOnly
        IBasketService BasketService { get; } //ReadOnly
        ICacheService CacheService { get; } //ReadOnly
        IAuthService AuthService { get; } //ReadOnly
        IOrderService OrderService { get; } //ReadOnly

    }
}
