using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Domain.Entities;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        // Create order (End Point)
        [HttpPost] // POST : BaseUrl/api/orders
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
            
        }

        //get all delivery methods
        [HttpGet("deliveryMethods")] // GET: BaseUrl/api/orders/deliveryMethods
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await _serviceManager.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }


        //get all orders for specific user


        [HttpGet] // GET: BaseUrl/api/orders
        [Authorize]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.GetOrdersForSpecificUserAsync(userEmailClaim.Value);
            return Ok(result);
        }


        [HttpGet("{id}")] // GET: BaseUrl/api/orders/id
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUserAsync(Guid id)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.GetOrderByIdForSpecificUserAsync(id, userEmailClaim.Value);
            return Ok(result);
        }




    }
}
