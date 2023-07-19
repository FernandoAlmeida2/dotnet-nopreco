using dotnet_nopreco.Dtos.Product;
using dotnet_nopreco.Models;

namespace dotnet_nopreco.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<ServiceResponse<List<Product>>> GetAll()
        {
            var response = new ServiceResponse<List<Product>>();
            response.Data = await _productRepo.FindAll();

            return response;
        }

        public async Task<ServiceResponse<int>> PostProduct(ProductReqDto newProduct)
        {
            var response = new ServiceResponse<int>();

            try
            {
                if (await _productRepo.FindByName(newProduct.Name) is not null)
                {
                    throw new Exception("A product with this name is already saved.");
                }
                
                var product = new Product {
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    ImageUrl = newProduct.ImageUrl,
                    Price = newProduct.Price,
                    Category = newProduct.Category
                };

                response.Data = await _productRepo.SaveProduct(product);
                response.Message = "Product saved successfully!";
            }
            catch (Exception Ex) { response.HandleError(Ex.Message); }

            return response;
        }
    }
}