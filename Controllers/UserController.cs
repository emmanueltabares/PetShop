using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.ViewModel;
using PetShop.ViewModel.UserViewModels;
using PetShop.Data;
using PetShop.Models;
using System.Globalization;

namespace PetShop.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public UserController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager,
        ApplicationDbContext context
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Index(string? filter)
    {
        var userModel = new UserListViewModel() {};

        if(!string.IsNullOrEmpty(filter)) {

            var users = _userManager.Users.Where(u => u.UserName.Contains(filter)).ToList();
            userModel.Users = users.Select(u => new UserViewModel {
                Id = u.Id,
                UserName = u.NormalizedUserName,
                Email = u.Email,
                Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault() ?? string.Empty
            }).ToList();

            userModel.LoggedInUserId = _userManager.GetUserAsync(User).Result.Id;
            
            return View(userModel);
        } else {
            var users = _userManager.Users.ToList();
            userModel.Users = users.Select(u => new UserViewModel {
                Id = u.Id,
                UserName = u.NormalizedUserName,
                Email = u.Email,
                Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault() ?? string.Empty
            }).ToList();
            
            userModel.LoggedInUserId = _userManager.GetUserAsync(User).Result.Id;
            return View(userModel);
        }
    }

    public IActionResult Create()
    {
        var userViewModel = new UserCreateViewModel
        {
            Roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList()
        };

        return View(userViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if(!ModelState.IsValid) return View(model);

        var user = new IdentityUser {
            Email = model.Email,
            PhoneNumber = model.Phone,
        };

        var normalizedUserName = string.Concat(model.UserName.Split(' ').Select(word =>
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower())));
        
        user.UserName = normalizedUserName;

        var result = await _userManager.CreateAsync(user, model.Password);
        if(result.Succeeded) {

            if(!string.IsNullOrEmpty(model.RoleId)) {
                var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                if(role != null) {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            TempData["SuccessMessage"] = "Usuario creado correctamente";
            return RedirectToAction(nameof(Index));
        } else {
            TempData["ErrorMessage"] = "No se pudo crear el usuario";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if(user == null) return NotFound();

        var userViewModel = new UserEditViewModel() {
            Id = user.Id,
            UserName = user.NormalizedUserName,
            Phone = user.PhoneNumber,
            Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? string.Empty,
            RoleId = _userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? string.Empty,
            Roles = _roleManager.Roles.Select(r => new SelectListItem {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList()
        };
     
        return View(userViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditConfirmed(UserEditViewModel model)
    {

        if(!ModelState.IsValid) return View(model);
        
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound();

        user.PhoneNumber = model.Phone;
        if(!string.IsNullOrEmpty(model.UserName)) {

            var normalizedUserName = string.Concat(model.UserName.Split(' ').Select(word =>
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower())));
            user.UserName = normalizedUserName;
        } else {
            TempData["ErrorMessage"] = "El nombre de usuario no puede estar vacío";
            return View(model);
        }

        var result = await _userManager.UpdateAsync(user);
        if(result.Succeeded) {

            if(!string.IsNullOrEmpty(model.RoleId)) {
                var newRole = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                if(newRole != null) {
                    var currentRole = await _userManager.GetRolesAsync(user);
                    if(currentRole.Count == 0) {
                         await _userManager.AddToRoleAsync(user, newRole.Name);
                    } else if(currentRole.FirstOrDefault() != newRole.Name) {
                        await _userManager.RemoveFromRoleAsync(user, currentRole.FirstOrDefault());
                        await _userManager.AddToRoleAsync(user, newRole.Name);
                    }
                } 
            }

            TempData["SuccessMessage"] = "Usuario actualizado correctamente";
            return RedirectToAction(nameof(Index));
        } else {
            TempData["ErrorMessage"] = "No se pudo actualizar el usuario";
            return View(model);
        }
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
            // userDetailviewModel.Role = role.ToList();

            return View(userDetailviewModel);
        }

    public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View();

            var userViewModel = new UserDeleteViewModel() {
                Id = user.Id,
                Email = user.Email,
            };

            return View(userViewModel);
        }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (id == null) return NotFound();
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View();

            try {
                await _userManager.DeleteAsync(user);
                TempData["SuccessMessage"] = "Usuario eliminado correctamente";
                return RedirectToAction(nameof(Index));
            } catch (Exception) {

                var errorViewModel = new ErrorViewModel() {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View(errorViewModel);
            }
        }
}