using Microsoft.AspNetCore.Identity;

namespace PetShop.ViewModel.RoleViewModels;

public class RoleListViewModel {
    public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();

    public string? Filter { get; set; }
}

