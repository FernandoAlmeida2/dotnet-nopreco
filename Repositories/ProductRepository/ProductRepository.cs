using dotnet_nopreco.Dtos.Product;
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

        public async Task<Product?> FindById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> SaveProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> UpdateProduct(int id, ProductReqDto updatedProduct)
        {
            var product = await FindById(id);
            if(product is null) return false;

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.ImageUrl = updatedProduct.ImageUrl;
            product.Price = updatedProduct.Price;
            product.Category = updatedProduct.Category;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await FindById(id);
            if(product is null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}