using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.UserViewModels;

public class UserCreateViewModel
{
    public string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required int RoleId { get; set; }
    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
}