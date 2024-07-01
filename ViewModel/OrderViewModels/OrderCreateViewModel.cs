using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.OrderViewModels;

public class OrderCreateViewModel {

    public List<OrderProductViewModel> OrderProducts { get; set; } = new List<OrderProductViewModel>();
    public int UserId { get; set; }
}   
