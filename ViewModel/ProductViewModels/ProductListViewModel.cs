using PetShop.Models;
namespace PetShop.ViewModel.ProductViewModels;

public class ProductListViewModel {

    public List<Product> Products { get; set; } = new List<Product>();

    public string? Filter { get; set; }

}