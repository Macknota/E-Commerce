using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
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
        //We Must Declare The Verb

        [HttpGet] //GET: baseUrl/api/products
        public async Task<IActionResult> GetAllProducts() //End Point 1
        {
            //I have made that logic Before in ProductService and to connect it i need object from service Manager

            var result = await _serviceManager.ProductService.GetAllProductsAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);  //200
        }


        [HttpGet ("{id}")] //GET: baseUrl/api/products/5
        public async Task<IActionResult> GetProductById(int? id) //End Point 2
        {
            if(id is null) return BadRequest();

            var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);
            
            if (result is null) return NotFound(); //404

            return Ok(result);  //200
        }


        [HttpGet ("brands")] //GET: baseUrl/api/products/brands
        public async Task<IActionResult> GetAllBrands() //End Point 3
        {

            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);  //200
        }


        [HttpGet ("types")] //GET: baseUrl/api/products/brands
        public async Task<IActionResult> GetAllTypes() //End Point 4
        {

            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);  //200
        }

    }
}
