using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order() //This ctor For Ef Core for Mapping
        {

        }

        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string? paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } // One column
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; // One column
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // One column
        public OrderAddress ShippingAddress { get; set; } // Many columns Part Of Order Table
        public DeliveryMethod DeliveryMethod { get; set; } // Navigational Property
        public int DeliveryMethodId { get; set; } //FK
        public ICollection<OrderItem> Items { get; set; } // Navigational Property
        public decimal SubTotal { get; set; } // Price * Quantity


        //[NotMapped] // derived Attribute
        //public decimal Total { get; set; } // SubTotal + Delivery Method Cost

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price; // Not Mapped As SP


        //Add This for PaymentIntent
        public string? PaymentIntentId { get; set; }


    }
}
