using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Presentation.Attributes;
using Store.G02.Services.Abstractions;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Products;
using Store.G02.Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    //API: Is public NonStatic Function => from type => IActionResult 
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase //Don't forget DI for IServiceManager
    {

        //Priceasc
        //Pricedesc
        //nameasc


        //We Must Declare The Verb

        [HttpGet] //GET: baseUrl/api/products
        [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError ,Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest ,Type = typeof(ErrorDetails))]
        [Cache(50)]
        //[Authorize]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters parameters) //End Point 1
        {
            //I have made that logic Before in ProductService and to connect it i need object from service Manager
            
            var result = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
            //if (result is null) return BadRequest(); //400
            return Ok(result);  //200
        }


        [HttpGet ("{id}")] //GET: baseUrl/api/products/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<ProductResponse>> GetProductById(int? id) //End Point 2
        {
            if(id is null) return BadRequest();

            var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);
            
            //if (result is null) return NotFound(); //404

            return Ok(result);  //200
        }




        [HttpGet ("brands")] //GET: baseUrl/api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<BrandTypeResponse>> GetAllBrands() //End Point 3
        {

            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);  //200
        }


        [HttpGet ("types")] //GET: baseUrl/api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<BrandTypeResponse>> GetAllTypes() //End Point 4
        {

            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);  //200
        }

    }
}
