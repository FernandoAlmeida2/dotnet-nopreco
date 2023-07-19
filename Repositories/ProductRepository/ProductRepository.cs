using dotnet_nopreco.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_nopreco.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> FindAll()
        {
            return await _context.Products.OrderBy(p => p.Category).ToListAsync();
        }

        public async Task<Product?> FindByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<int> SaveProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}