using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductsController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index(string name, int company = 0, int page = 1,
			SortState sortOrder = SortState.NameAsc)
		{
			int pageSize = 5;
        
			IQueryable<Product> products = _context.Product.Include(x => x.ProductCategory);
 
			if (company != 0)
			{
				products = products.Where(p => p.ProductCategoryId == company);
			}
			if (!string.IsNullOrEmpty(name))
			{
				products = products.Where(p => p.Name!.Contains(name));
			}
        
			switch (sortOrder)
			{
				case SortState.NameDesc:
					products = products.OrderByDescending(s => s.Name);
					break;
				case SortState.AgeAsc:
					products = products.OrderBy(s => s.Price);
					break;
				case SortState.AgeDesc:
					products = products.OrderByDescending(s => s.Price);
					break;
				case SortState.CompanyAsc:
					products = products.OrderBy(s => s.ProductCategory!.Name);
					break;
				case SortState.CompanyDesc:
					products = products.OrderByDescending(s => s.ProductCategory!.Name);
					break;
				default:
					products = products.OrderBy(s => s.Name);
					break;
			}
        
			var count = await products.CountAsync();
			var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
			IndexViewModel viewModel = new IndexViewModel(
				items,
				new PageViewModel(count, page, pageSize),
				new FilterViewModel(_context.ProductCategory.ToList(), company, name),
				new SortViewModel(sortOrder)
			);
			return View(viewModel);
		}
		// // GET: Products
		// public async Task<IActionResult> Index()
		// {
		// 	var webApplication1Context = _context.Product.Include(p => p.ProductCategory);
		// 	return View(await webApplication1Context.ToListAsync());
		// }

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Product == null)
			{
				return NotFound();
			}

			var product = await _context.Product
				.Include(p => p.ProductCategory)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			ViewData["ProductCategoryId"] = new SelectList(_context.Set<ProductCategory>(), "Id", "Name");
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Price,ProductCategoryId,PhotoUrl")] Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Add(product);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["ProductCategoryId"] = new SelectList(_context.Set<ProductCategory>(), "Id", "Id", product.ProductCategoryId);
			return View(product);
		}

		// GET: Products/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Product == null)
			{
				return NotFound();
			}

			var product = await _context.Product.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["ProductCategoryId"] = new SelectList(_context.Set<ProductCategory>(), "Id", "Name", product.ProductCategoryId);
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ProductCategoryId,PhotoUrl")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(product);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductExists(product.Id))
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
			ViewData["ProductCategoryId"] = new SelectList(_context.Set<ProductCategory>(), "Id", "Id", product.ProductCategoryId);
			return View(product);
		}

		// GET: Products/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Product == null)
			{
				return NotFound();
			}

			var product = await _context.Product
				.Include(p => p.ProductCategory)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Product == null)
			{
				return Problem("Entity set 'WebApplication1Context.Product'  is null.");
			}
			var product = await _context.Product.FindAsync(id);
			if (product != null)
			{
				_context.Product.Remove(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(int id)
		{
			return _context.Product.Any(e => e.Id == id);
		}
	}
}
