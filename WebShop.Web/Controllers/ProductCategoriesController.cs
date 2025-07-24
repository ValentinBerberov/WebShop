using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop.Data.Entities;
using WebShop.Services.EntityServices;
using WebShop.Web.Data;

namespace WebShop.Web.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductCategoryService _service;

        public ProductCategoriesController(ApplicationDbContext context,
            ProductCategoryService service)
        {
            _context = context;
            _service = service;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllProductCategoriesAsync());
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(Guid? productId, Guid? categoryId)
        {
            if (productId == null || categoryId == null)
            {
                return NotFound();
            }

            var productCategory = await _service.GetProductCategoryByIdAsync(productId, categoryId);

            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateProductCategoryAsync(productCategory);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productCategory.ProductId);
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? productId, Guid? categoryId)
        {
            if (productId == null || categoryId == null)
            {
                return NotFound();
            }

            var productCategory = await _service.GetProductCategoryByIdAsync(productId, categoryId);

            if (productCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productCategory.ProductId);
            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid productId, Guid categoryId, [Bind("ProductId,CategoryId")] ProductCategory productCategory)
        {
            if (productId != productCategory.ProductId ||
                categoryId != productCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateProductCategoryAsync(productId, categoryId, productCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productCategory.ProductId);
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? productId, Guid? categoryId)
        {
            if (productId == null || categoryId == null)
            {
                return NotFound();
            }

            var productCategory = await _service.GetProductCategoryByIdAsync(productId, categoryId);

            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid productId, Guid categoryId)
        {
            var productCategory = await _service.GetProductCategoryByIdAsync(productId, categoryId);

            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(Guid id)
        {
            return _context.ProductCategories.Any(e => e.ProductId == id);
        }
    }
}
