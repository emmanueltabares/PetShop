using PetShop.Models;
namespace PetShop.Interfaces;

public interface IMakeService {
    void Create(Make obj);
    List<Make> GetAll();
    List<Make> GetAll(string filter);
    void Update(Make obj);
    void Delete(int id);
    Make? GetById(int id);
}