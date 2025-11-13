using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Products
{
    public interface IProductService
    {
        
        /* Last Step in Bussines Logic Layer 
         * 1- Create Classes : we will write inside it the Logic of end points
         * 2- ProductService : Services.Abstractions => Folder: Products => IProductService => Signature of 4 methods (Get,GetAll......)
         * But first we need tp create DTO : 
         * 
         */
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
