using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.Dtos.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } // One column
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; // One column
        public OrderAddressDto ShippingAddress { get; set; } // Many columns Part Of Order Table
        public string DeliveryMethod { get; set; } // Delivery Method Name
        public ICollection<OrderItemDto> Items { get; set; } 
        public decimal SubTotal { get; set; } // Price * Quantity
        public decimal Total { get; set; } // SubTotal + Delivery Method Cost


    }
}
