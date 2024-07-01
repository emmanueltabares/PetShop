using Microsoft.AspNetCore.Mvc;
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
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}