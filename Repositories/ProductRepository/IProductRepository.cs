using dotnet_nopreco.Dtos.Product;
using dotnet_nopreco.Models;

namespace dotnet_nopreco.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<int> SaveProduct(Product product);
        Task<Product?> FindByName(string name);
        Task<Product?> FindById(int id);
        Task<List<Product>> FindAll();
        Task<bool> UpdateProduct(int id, ProductReqDto updatedProduct);
    }
}