using PetShop.ViewModel.OrderViewModels;

namespace PetShop.ViewModel.OrderViewModels;

public class OrderDetailsViewModel {
    public List<OrderProductViewModel> OrderProducts { get; set; } = new List<OrderProductViewModel>();
    public string UserName { get; set; }

    public float TotalPrice => OrderProducts.Sum(x => x.Price * x.Quantity);

    public int TotalProducts => OrderProducts.Sum(x => x.Quantity);

}