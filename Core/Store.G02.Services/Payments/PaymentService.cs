using Store.G02.Domain.Contracts;
using Store.G02.Services.Abstractions.Payments;
using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Payments
{
    public class PaymentService(IBasketRepository _basketRepository) : IPaymentService
    {
        public async Task<BasketDto> CreatePaymentIntentAsync(string basketId)
        {
            // Calculate Amount = SubTotal + Delivery Method Cost
            // Send Amount TO stripe (To Create Recipe and Payment Intent)
            var basket = await _basketRepository.GetBasketAsync(basketId);


        }
    }
}
