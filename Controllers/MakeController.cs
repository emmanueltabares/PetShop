using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.ViewModel.MakeViewModels;

public class MakeController : Controller
{
    private readonly IMakeService _makeService;

    public MakeController(IMakeService makeService)
    {
        _makeService = makeService;
    }

    public IActionResult Index()
    {
        var makes = _makeService.GetAll();
        var model = new MakeListViewModel()
        {
            Makes = makes
        };

        return View(model);
    }

    public IActionResult Create()
    {
        var model = new MakeCreateViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MakeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {

            var make = new Make()
            {
                Name = model.Name,
                Description = ""
            };

            _makeService.Create(make);
            TempData["SuccessMessage"] = "Fabricante agregado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    // GET: Make/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var category =  _makeService.GetById(id.Value);
        if (category == null) return NotFound();

        var model = new MakeEditViewModel()
        {
            MakeId = category.MakeId,
            Name = category.Name
        };
        
        return View(model);
    }

    // POST: Make/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, MakeEditViewModel makeModel)
    {
        if (id != makeModel.MakeId) return NotFound();
        if (ModelState.IsValid)
        {
            var make = new Make()
            {
                MakeId = makeModel.MakeId,
                Name = makeModel.Name
            };

            _makeService.Update(make);
            TempData["SuccessMessage"] = "Fabricante actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        return View(makeModel);
    }

    // GET: Make/Delete/5
    public IActionResult Delete(int id)
    {
        var make = _makeService.GetById(id);

        if (make == null) return NotFound();
        var model = new MakeDeleteViewModel
        {
            MakeId = make.MakeId,
            Name = make.Name
        };

        return View(model);
    }

    // POST: Make/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var animalCategory = _makeService.GetById(id);
            if(animalCategory == null) return NotFound();
            
            try
            {
                _makeService.Delete(id);
                TempData["SuccessMessage"] = "Fabricante eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "No se puede eliminar el fabricante porque tiene productos asociados.";
                var model = new MakeDeleteViewModel() {
                    MakeId = animalCategory.MakeId,
                    Name = animalCategory.Name
                };
                return View(model);
            }

            return RedirectToAction(nameof(Index));
    }
}