using PetShop.Models;

namespace PetShop.ViewModel.OrderViewModels;

public class OrderProductViewModel {

    public int ProductId { get; set; }
    public string Name { get; set; }

    public float Price { get; set; }

    public int Quantity { get; set; }


}