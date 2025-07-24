using Microsoft.EntityFrameworkCore;
using WebShop.Data.Entities;
using WebShop.Web.Data;

namespace WebShop.Services.EntityServices
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid? id)
        {
            return await _context.Products.FindAsync(id);
        }


        public async Task CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            _context.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
