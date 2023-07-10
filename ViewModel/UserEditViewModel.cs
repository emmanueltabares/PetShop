using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel;

public class UserEditViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Rol { get; set; }
    public SelectList Roles { get; set; }
}