using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.UserViewModels;

public class UserDetailViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
}