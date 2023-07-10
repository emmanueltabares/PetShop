using PetShop.Models;

namespace PetShop.ViewModel;

public class CartViewModel
{
    public List<Product> Products { get; set; } = new List<Product>();
}