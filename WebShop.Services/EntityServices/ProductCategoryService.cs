using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data.Entities;
using WebShop.Web.Data;

namespace WebShop.Services.EntityServices
{
    public class ProductCategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(ApplicationDbContext context,
            ILogger<ProductCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await _context.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductCategory?> GetProductCategoryByIdAsync(Guid? productId, Guid? categoryId)
        {
            return await _context.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        public async Task CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.Add(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(Guid productId, Guid categoryId, ProductCategory productCategory)
        {
            var oldProductCategory = await GetProductCategoryByIdAsync(productId, categoryId);

            if (oldProductCategory != null)
            {
                _context.Remove(oldProductCategory);
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("ProductCategory object could not be updated with values:\n" +
                    "\t{ProductId}\n\t{CategoryId}", productCategory.ProductId, productCategory.CategoryId);
            }
            
        }
    }
}
