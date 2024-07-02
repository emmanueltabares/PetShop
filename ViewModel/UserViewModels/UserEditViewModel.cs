using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.UserViewModels;

public class UserEditViewModel
{
    public string Id { get; set; }
    public required string UserName { get; set; }
    public string? Phone { get; set; }

    public string? Role { get; set; }
    public string? RoleId { get; set; }
    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
}