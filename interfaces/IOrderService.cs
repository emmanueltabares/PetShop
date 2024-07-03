using PetShop.Models;
namespace PetShop.Interfaces;

public interface IOrderService
{
    void Create(Order order);
    List<Order> GetAll();
    List<Order> GetAll(string filter);
    Order GetById(int id);
    void Delete(int id);
}