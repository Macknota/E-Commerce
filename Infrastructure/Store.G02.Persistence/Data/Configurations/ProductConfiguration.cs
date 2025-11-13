using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Store.G02.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Data.Configurations
{
    //Step 2 :
    public class ProductConfiguration : IEntityTypeConfiguration<Product> //Generic Interface For Configuration
    {
        //Note That it can see The Product of Domain_Layer Because of :Dependecies => Services => Domain....
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(P => P.Description).HasColumnType("varchar").HasMaxLength(512);
            builder.Property(P => P.PictureUrl).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

            //Relationship Configuration

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(P => P.Type)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
