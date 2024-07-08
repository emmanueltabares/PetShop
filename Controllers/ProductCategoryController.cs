using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.ViewModel;
using PetShop.ViewModel.ProductCategoryViewModels;

namespace PetShop.Controllers
{
    [Authorize(Roles = "Administrador")] 
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _productCategoryService = categoryService;
        }

        /**
        * GET: Category Index
        * This method returns a view with a list of categories.
        * If the filter parameter is not null or empty, the method will return a list of categories that match the filter.
        */
        public IActionResult Index(string? filter)
		{

			var categoriesListViewModel = new ProductCategoryListViewModel();

			if(!string.IsNullOrEmpty(filter)) {
				var filterCategories = _productCategoryService.GetAll(filter);
				categoriesListViewModel.Categories = filterCategories;

			} else {
				var categories = _productCategoryService.GetAll();
				categoriesListViewModel.Categories = categories;
			}

			return View(categoriesListViewModel);
		}

        // GET: Category/Create
        public IActionResult Create()
        {
            var model = new ProductCategoryCreateViewModel();
            return View(model);
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryCreateViewModel model)
        {
            if(ModelState.IsValid) {
                try {
                    var category = new ProductCategory() {
                        Name = model.Name
                    };

                    _productCategoryService.Create(category);
                    TempData["SuccessMessage"] = "Categoría de producto agregada correctamente.";
                    return RedirectToAction(nameof(Index));

                } catch (Exception ex) {
                    TempData["ErrorMessage"] = "No se pudo agregar la categoría de producto." + ex.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var category = _productCategoryService.GetById(id.Value);
            if (category == null) return NotFound();

            var model = new ProductCategoryEditViewModel() {
                ProductCategoryId = category.ProductCategoryId,
                Name = category.Name
            };
            
            return View(model);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] ProductCategory model)
        {
            if (id == null) return NotFound();

            ModelState.Remove("Products");

            if(ModelState.IsValid) {
                try {
                    var category = _productCategoryService.GetById(id);
                    if(category == null) return NotFound();

                    category.Name = model.Name;
                    _productCategoryService.Update(category);
                    TempData["SuccessMessage"] = "Categoría de producto actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                } catch (Exception ex) {
                    TempData["ErrorMessage"] = "No se pudo actualizar la categoría de producto." + ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var category = _productCategoryService.GetById(id.Value);
            if (category == null) return NotFound();

            var model = new ProductCategoryDeleteViewModel() {
                ProductCategoryId = category.ProductCategoryId,
                Name = category.Name
            };

            return View(model);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productCategory = _productCategoryService.GetById(id);
            if(productCategory == null) return NotFound();
            
            try
            {
                _productCategoryService.Delete(id);
                TempData["SuccessMessage"] = "Categoría eliminada correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "No se puede eliminar la categoría porque tiene productos asociados.";
                var model = new ProductCategoryDeleteViewModel() {
                    ProductCategoryId = productCategory.ProductCategoryId,
                    Name = productCategory.Name
                };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
