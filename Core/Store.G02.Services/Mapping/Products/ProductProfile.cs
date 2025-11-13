using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G02.Domain.Entities.Products;
using Store.G02.Services.Products;
using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Mapping.Products
{
    //To Make a Profile class it need to inherit from profile Class from AutoMapper Package
    public class ProductProfile : Profile 
    {
        public ProductProfile(IConfiguration configuration)
        {
            //The output from this mapping is object from ProductResponse :
            //How it Works :
            //He search in the Left side for a property with the same name and the same dataType and put it in the right side

            CreateMap<Product, ProductResponse>()
                    .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name)) // Brand(ProductResponse) map with Brand(Product)
                    .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name))// Type(ProductResponse) map with Type(Product)
                    //.ForMember(D => D.PictureUrl, O => O.MapFrom(S => $"{configuration["BaseUrl"]}/{S.PictureUrl}"));
                    .ForMember(D => D.PictureUrl, O => O.MapFrom(new ProductPictureUrlResolver(configuration)));
           
            
            
            
            //--------------Add Base URL To PictureURL

            //----->Before :     "pictureUrl": "images/products/ItalianChickenMarinade.png",
            //----->After :       "pictureUrl": "https://localhost:7295/images/products/ItalianChickenMarinade.png",






            //for detect some prop manually 

            CreateMap<ProductBrand, BrandTypeResponse>(); //Name 
            CreateMap<ProductType, BrandTypeResponse>(); //Name



        }

    }
}
