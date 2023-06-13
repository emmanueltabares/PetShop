using PetShop.Models;

namespace PetShop.ViewModel;

public class OrderCreateViewModel {

    public List<Product> products { get; set; } = new List<Product>();

    public int quantity { get; set; }

}