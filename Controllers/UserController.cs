using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.ViewModel;

namespace PetShop.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(
        ILogger<HomeController> logger,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        //listar todos los usuarios
        var users = _userManager.Users.ToList();
        return View(users);
    }

    // [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if(user != null) {
            var userViewModel = new UserEditViewModel();
            userViewModel.UserName = user.UserName ?? string.Empty;
            userViewModel.Email = user.Email ?? string.Empty;
            userViewModel.Roles = new SelectList(_roleManager.Roles.ToList());

            return View(userViewModel);
        }
        return View();
    }

    // [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user != null)
        {
            user.Email = model.Email;
            user.UserName = model.UserName;
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(string? id)
        {
            if (id == null) return NotFound();
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View();

            var userDetailviewModel = new UserDetailViewModel();
            userDetailviewModel.UserName = user.UserName ?? string.Empty; 
            userDetailviewModel.Email = user.Email ?? string.Empty;
            userDetailviewModel.PhoneNumber = user.PhoneNumber ?? string.Empty;

            var role = await _userManager.GetRolesAsync(user);
            userDetailviewModel.Roles = role.ToList();

            return View(userDetailviewModel);
        }

    // [Authorize(Roles = "Administrador")]
    // [HttpPost]
    public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View();

            await _userManager.DeleteAsync(user);

            return View("index");
        }
}