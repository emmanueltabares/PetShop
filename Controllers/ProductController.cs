using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PetShop.Data;
using PetShop.Models;
using PetShop.ViewModel;
using PetShop.Interfaces;
using PetShop.ViewModel.ProductViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PetShop.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IProductCategoryService _productCategoryService;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IAnimalCategoryService _animalCategoryService;
		private readonly IMakeService _makeService;

		public ProductController(
				IProductService productService,
				IProductCategoryService categoryService,
				IAnimalCategoryService animalCategoryService,
				IMakeService makeService,
				UserManager<IdentityUser> userManager
				)
		{
			_productService = productService;
			_productCategoryService = categoryService;
			_animalCategoryService = animalCategoryService;
			_makeService = makeService;
			_userManager = userManager;
		}

		// GET: Product
		public IActionResult Index(string? filter)
		{

			var productListViewModel = new ProductListViewModel();

			if(!string.IsNullOrEmpty(filter)) {
				var filterProducts = _productService.GetAll(filter);
				productListViewModel.Products = filterProducts;

			} else {
				var products = _productService.GetAll();
				productListViewModel.Products = products;
			}

			return View(productListViewModel);
		}

		// GET: Product/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null) return NotFound();

			var product = _productService.GetById(id.Value);
			if (product == null) return NotFound();

			return View(product);
		}

		// GET: Product/Create
		public IActionResult Create()
		{

			var productCategories = _productCategoryService.GetAll();
			var animalCategories = _animalCategoryService.GetAll();
			var makes = _makeService.GetAll();

			var productCreateViewModel = new ProductCreateViewModel
			{
				ProductCategories = productCategories.Select(pc => new SelectListItem
				{
					Value = pc.ProductCategoryId.ToString(),
					Text = pc.Name
				}).ToList(),
				AnimalCategories = animalCategories.Select(ac => new SelectListItem
				{
					Value = ac.AnimalCategoryId.ToString(),
					Text = ac.Name
				}).ToList(),
				Makes = makes.Select(m => new SelectListItem
				{
					Value = m.MakeId.ToString(),
					Text = m.Name
				}).ToList()
			};

			return View(productCreateViewModel);
		}

		// POST: Product/Create
		[HttpPost]
		public IActionResult Create(ProductCreateViewModel product) {
			
			if (product is null) {
				throw new ArgumentNullException(nameof(product));
			}

			ModelState.Remove("ProductCategories");
			ModelState.Remove("AnimalCategories");
			ModelState.Remove("Makes");

			if (ModelState.IsValid) {
				var productModel = new Product()
				{
					Name = product.Name,
					Price = product.Price,
					Stock = product.Stock,
					ProductCategoryId = product.ProductCategoryId,
					AnimalCategoryId = product.AnimalCategoryId,
					MakeId = product.MakeId,
					Description = product.Description ?? "",
					Cod = product.Cod ?? 0,
				};

				_productService.Create(productModel);
				TempData["SuccessMessage"] = "Producto agregado correctamente.";
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Index));
		}

		// GET: Product/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null) return NotFound();

			var product = _productService.GetById(id.Value);
			if (product == null) return NotFound();

			var model = new ProductEditViewModel
			{
				ProductId = product.ProductId,
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
				Description = product.Description,
				Cod = product.Cod,
			};

			return View(model);
		}

		// POST: Product/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Product product)
		{
			if (id != product.ProductId) return NotFound();

			ModelState.Remove("ProductCategories");
			ModelState.Remove("AnimalCategories");
			ModelState.Remove("Make");
			ModelState.Remove("OrderDetails");
			ModelState.Remove("AnimalCategory");
			ModelState.Remove("ProductCategory");

			if (ModelState.IsValid)
			{
				try
				{
					_productService.Update(product);
					TempData["SuccessMessage"] = "Producto actualizado correctamente.";
					return RedirectToAction(nameof(Index));
					
				}
				catch (DbUpdateConcurrencyException)
				{
					TempData["ErrorMessage"] = "Error al intentar actualizar el producto.";
					return RedirectToAction(nameof(Index));
				}
			}

			TempData["ErrorMessage"] = "Complete los datos requeridos";
			
			var model = new ProductEditViewModel {
				ProductId = product.ProductId,
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
				Description = product.Description,
				Cod = product.Cod,
			};
			return View(model);
		}

		// GET: Product/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null) return NotFound();

			var product = _productService.GetById(id.Value);
			if (product == null) return NotFound();

			var model = new ProductDeleteViewModel
			{
				ProductId = product.ProductId,
				Name = product.Name
			};

			return View(model);
		}

		// POST: Product/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			try {
				var product = _productService.GetById(id);
				if(product == null) return NotFound();

				try {
					_productService.Delete(id);
					TempData["SuccessMessage"] = "Producto eliminado correctamente.";
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateException ex) {
					TempData["ErrorMessage"] = "No se puede eliminar el producto porque tiene órdenes asociadas.";
					var model = new ProductDeleteViewModel
						{
							ProductId = product.ProductId,
							Name = product.Name
						};
					return View(model);
				}
			} catch (Exception ex) {
				TempData["ErrorMessage"] = "Error al intentar eliminar el producto" + ex.Message;
				return RedirectToAction(nameof(Index));
			}
		}

		private bool ProductExists(int id)
		{
			return _productService.GetById(id) != null;
		}

	}
}
