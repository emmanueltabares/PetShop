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
using PetShop.ViewModel.AnimalCategoryViewModels;

namespace PetShop.Controllers
{
    [Authorize]
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
                TempData["SuccessMessage"] = "Categoría de animal agregada correctamente.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var category =  _animalCategoryService.GetById(id.Value);
            if (category == null) return NotFound();

            var model = new AnimalCategoryEditViewModel()
            {
                AnimalCategoryId = category.AnimalCategoryId,
                Name = category.Name
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AnimalCategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new AnimalCategory()
                {
                    AnimalCategoryId = model.AnimalCategoryId,
                    Name = model.Name
                };

                _animalCategoryService.Update(category);
                TempData["SuccessMessage"] = "Categoría de animal actualizada correctamente.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var category = _animalCategoryService.GetById(id);

            if (category == null) return NotFound();
            var model = new AnimalCategoryDeleteViewModel
            {
                AnimalCategoryId = category.AnimalCategoryId,
                Name = category.Name
            };

            return View(model);
        }

        // POST: AnimalCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var animalCategory = _animalCategoryService.GetById(id);
            if(animalCategory == null) return NotFound();
            
            try
            {
                _animalCategoryService.Delete(id);
                TempData["SuccessMessage"] = "Categoría eliminada correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "No se puede eliminar la categoría porque tiene productos asociados.";
                var model = new AnimalCategoryDeleteViewModel() {
                    AnimalCategoryId = animalCategory.AnimalCategoryId,
                    Name = animalCategory.Name
                };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
