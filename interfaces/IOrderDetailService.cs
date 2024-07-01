using PetShop.Models;
namespace PetShop.Interfaces;

public interface IOrderDetailService {

    void Create(OrderDetail obj);
    void GetById(int id);

}