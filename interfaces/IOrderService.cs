using PetShop.Models;
namespace PetShop.Interfaces;

public interface IOrderService
{
    void Create(Order order);
    List<Order> GetAll();
}