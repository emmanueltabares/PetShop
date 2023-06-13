namespace PetShop.ViewModel;

public class UserDetailViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public virtual List<string> Roles { get; set; }
}