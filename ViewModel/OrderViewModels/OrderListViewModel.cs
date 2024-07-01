using PetShop.Models;

namespace PetShop.ViewModel.OrderViewModels;

public class OrderListViewModel {

    public List<Order> Orders { get; set; } = new List<Order>();

    public string? Filter { get; set; }

}