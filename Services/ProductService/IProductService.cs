using dotnet_nopreco.Dtos.Product;
using dotnet_nopreco.Models;

namespace dotnet_nopreco.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<int>> PostProduct(ProductReqDto newProduct);
        Task<ServiceResponse<List<Product>>> GetAll();
        Task<ServiceResponse<string>> PutProduct(int id, ProductReqDto updatedProduct);
        Task<ServiceResponse<string>> DeleteProduct(int id);
    }
}