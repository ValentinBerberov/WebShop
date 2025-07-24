using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data.Entities;
using WebShop.Web.Data;

namespace WebShop.Services.EntityServices
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(Guid? id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

    }
}
