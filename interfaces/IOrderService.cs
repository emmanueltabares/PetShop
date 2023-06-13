using PetShop.Models;
using PetShop.ViewModel;

public interface IOrderService
{
    void Create(Order order);
    List<Order> GetAll();
}