using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using PetShop.ViewModel;
using PetShop.ViewModel.RoleViewModels;
using PetShop.Models;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Controllers;

[Authorize (Roles = "Administrador")]
public class RoleController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(
        ILogger<HomeController> logger,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public IActionResult Index(string? filter)
    {
        var model = new RoleListViewModel();

        if(!string.IsNullOrEmpty(filter))
        {
            var roles = _roleManager.Roles.Where(r => r.Name.Contains(filter)).ToList();
            model.Roles = roles;
            return View(model);
        } else {
            var roles = _roleManager.Roles.ToList();
            model.Roles = roles;
            return View(model);
        }
    }

    public IActionResult Create()
    {
        var model = new RoleCreateViewModel();
        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateViewModel model)
    {
        if(!ModelState.IsValid) return View();
        
        if(string.IsNullOrEmpty(model.Name)) return View();
        
        var role = new IdentityRole(model.Name);
        var result = await _roleManager.CreateAsync(role);
        if(result.Succeeded) {
            TempData["SuccessMessage"] = "Rol agregado correctamente";
            return RedirectToAction(nameof(Index));
        } else {
            TempData["ErrorMessage"] = "Error al agregar el rol";
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Delete(string? id)
		{
			if (id == null) return NotFound();

			var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
			if (role == null) return NotFound();

			var model = new RoleDeleteViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

			return View(model);
		}

		// POST: Product/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
            if(id == null) return NotFound();

            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if(role == null) return NotFound();

            try {
			    var result = await _roleManager.DeleteAsync(role);
                if(result.Succeeded) {
                    TempData["SuccessMessage"] = "Rol eliminado correctamente";
                    return RedirectToAction(nameof(Index));
                } else {
                    TempData["ErrorMessage"] = "Error al eliminar el rol";
                    return RedirectToAction(nameof(Index));
                }

            } catch (DbUpdateException ex) {
                TempData["ErrorMessage"] = "No se puede eliminar el rol porque tiene usuarios asignados.";
                 var model = new RoleDeleteViewModel() {
                    Id = role.Id,
                    Name = role.Name
                };
                return View(model);
            }

		}
}