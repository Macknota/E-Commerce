using Store.G02.Domain.Entities.Products;
using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int,Product>
    {
        private ProductQueryParameters parameters;

        public ProductsWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }

     

        //Null & null ?
        //Filteration...
        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                //Filteration Logic

                P =>
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {

            //pageIndex = 3;
            //pageSize = 5 ;
            //Skip : 2 * 5 (PageIndex - 1) * PageSize
            //Take : 5

            ApplyPagination(parameters.PageSize, parameters.PageIndex);

            ApplySorting(parameters.Sort);
            ApplyIncludes();

        }


        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }


        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                //Sorting Logic

                //Priceasc
                //Pricedesc
                //nameasc


                //Check Value
                switch (sort.ToLower())
                {
                    case "priceasc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        //AddOrderByDescending = P => P.Price;
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        //OrderBy = P => P.Name; // The inherited property
                        AddOrderBy(P => P.Name); //Function
                        break;
                }


            }
            else
            {            //nameasc

                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);

            }
        }


    }
}
