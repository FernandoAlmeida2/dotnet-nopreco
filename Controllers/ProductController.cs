using dotnet_nopreco.Dtos.Product;
using dotnet_nopreco.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_nopreco.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> PostProduct(ProductReqDto newProduct) {
            var response = await _productService.PostProduct(newProduct);
            if(!response.Success) {
                if(response.Message == "A product with this name is already saved.")
                    return Conflict(response);
            }

            return Created("Save a product", response);
        }
    }
}