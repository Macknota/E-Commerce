using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Products;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Services.Mapping.Products;
using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Products
{
    internal class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        //Don't Forget To Allow DI
        //Implementation Of The Logic of the (4 EndPoints)

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
           var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync();

            //We need to mapping of IEnumerable<Product> to IEnumerable<ProductResponse>
            //We will use Auto Mapper => install package autoMapper => 1
            //We will Make a Mapping Profile that detect the way of Mapping => 2

           var result = _mapper.Map<IEnumerable<ProductResponse>>(products); //_mapper.Map<Destination>>(source);
           return result;

        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
           var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(id);
           var result = _mapper.Map<ProductResponse>(product);
           return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return result;
        }

     
    }
}
