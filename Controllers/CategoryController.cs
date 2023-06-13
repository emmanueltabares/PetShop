using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public IActionResult Index(string Filter)
        {
           var model = new CategoryViewModel();
            model.Category = _categoryService.GetAll();

            return View(model); 
        }

        // GET: Category/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var category = _categoryService.GetById(id.Value);
            if (category == null) return NotFound();

           var viewModel = new CategoryDetailViewModel();
            viewModel.Name = category.Name;
            viewModel.Products = category.Products != null ? category.Products : new List<Product>();

            return View(viewModel);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Category category)
        {
            ModelState.Remove("Products");
            if(ModelState.IsValid) {
               _categoryService.Create(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var category = _categoryService.GetById(id.Value);
            if (category == null) return NotFound();
            
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id) return NotFound();

            if(ModelState.IsValid) {
                try
                {
                    _categoryService.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id)) return NotFound();
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
            
            var category = _categoryService.GetById(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _categoryService.GetById(id) != null;
        }
    }
}
