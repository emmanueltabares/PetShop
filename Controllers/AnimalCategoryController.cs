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
using PetShop.ViewModel.AnimalCategoryViewModels;

namespace PetShop.Controllers
{
    public class AnimalCategoryController : Controller
    {
        private readonly IAnimalCategoryService _animalCategoryService;

        public AnimalCategoryController(IAnimalCategoryService categoryService)
        {
            _animalCategoryService = categoryService;
        }

        // GET: Category
        public IActionResult Index(string Filter)
        {
            var categories = _animalCategoryService.GetAll();
            var model = new AnimalCategoryListViewModel() {
                Categories = categories
            };

            return View(model);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            var model = new AnimalCategoryCreateViewModel();
            return View(model);
        }

        // POST: Category
        [HttpPost]
        public IActionResult Create(AnimalCategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var animalCategoryModel = new AnimalCategory()
                {
                    Name = model.Name
                };
                _animalCategoryService.Create(animalCategoryModel);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var product =  _animalCategoryService.GetById(id.Value);
            if (product == null) return NotFound();
            // ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", product.CategoryId);
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var category = _animalCategoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            // var viewModel = new AnimalCategoryDeleteViewModel
            // {
            //     AnimalCategoryId = category.AnimalCategoryId,
            //     Name = category.Name
            // };

            return View("Index");
        }

            // POST: AnimalCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _animalCategoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            _animalCategoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
