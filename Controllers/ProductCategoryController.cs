using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _productCategoryService = categoryService;
        }

        // GET: Category
        public IActionResult Index(string Filter)
        {
            var categories = _productCategoryService.GetAll();
            var model = new ProductCategoryListViewModel() {
                Categories = categories
            };
        
            return View(model); 
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

                var category = new ProductCategory() {
                    Name = model.Name
                };

               _productCategoryService.Create(category);
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var category = _productCategoryService.GetById(id.Value);
            if (category == null) return NotFound();
            
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] ProductCategory category)
        {
            if (id != category.ProductCategoryId) return NotFound();

            if(ModelState.IsValid) {
                try
                {
                    _productCategoryService.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ProductCategoryId)) return NotFound();
                    else throw;
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var category = _productCategoryService.GetById(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productCategoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _productCategoryService.GetById(id) != null;
        }
    }
}
