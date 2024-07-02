using Microsoft.AspNetCore.Identity;
namespace PetShop.ViewModel.UserViewModels;

public class UserListViewModel
{
    public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public string? Filter { get; set; }
}

public class UserViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }

    public string Email { get; set; }
    public string? Role { get; set; }
}