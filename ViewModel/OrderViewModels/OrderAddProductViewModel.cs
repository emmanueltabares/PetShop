using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models;
using PetShop.ViewModel.OrderViewModels;

namespace PetShop.ViewModel.OrderViewModels;

public class OrderAddProductViewModel {

    public List<OrderProductViewModel> OrderProducts { get; set; } = new List<OrderProductViewModel>();
    public int UserId { get; set; }
    public List<SelectListItem> AvailableProducts { get; set; } = new List<SelectListItem>();
    public int SelectedProductId { get; set; }
    public int Quantity { get; set; }

}   
