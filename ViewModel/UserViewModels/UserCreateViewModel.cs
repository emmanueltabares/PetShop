using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.UserViewModels;

public class UserCreateViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public string? RoleId { get; set; }
    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
}