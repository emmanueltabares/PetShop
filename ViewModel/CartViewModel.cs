using PetShop.Models;

namespace PetShop.ViewModel;

public class CartViewModel
{
    public List<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
}