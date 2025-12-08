using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G02.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // I need to tell him that the class(OrderAddress) is apart of Order Table
            builder.OwnsOne(O => O.ShippingAddress);

            //Relationship of DeliveryMethod (1 : M) 
            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId);

            //Relationship of DeliveryMethod (1 : M) 
            builder.HasMany(O => O.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
            // When delete a detected Order Remove with him All Items Related To This Order


            builder.Property(O => O.SubTotal)
                   .HasColumnType("decimal(18,2)");

        }
    }
}
