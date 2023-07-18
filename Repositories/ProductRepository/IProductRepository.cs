using dotnet_nopreco.Models;

namespace dotnet_nopreco.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<int> SaveProduct(Product product);
        Task<Product?> FindByName(string name);
    }
}