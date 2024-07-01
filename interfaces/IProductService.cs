using PetShop.Models;
namespace PetShop.Interfaces;

public interface IProductService {
  void Create(Product obj);
  List<Product> GetAll(string filter);
  List<Product> GetAll();
  void Update(Product obj);
  void Delete(int id);
  Product? GetById(int id);

}