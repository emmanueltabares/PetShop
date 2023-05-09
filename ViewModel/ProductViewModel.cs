using PetShop.Models;
namespace PetShop.ViewModel;

public class ProductViewModel {

    public List<Product> Products { get; set; } = new List<Product>();

    public string? Filter { get; set; }

}