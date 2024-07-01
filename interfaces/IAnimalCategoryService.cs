using PetShop.Models;
namespace PetShop.Interfaces;

public interface IAnimalCategoryService
{
    void Create(AnimalCategory category);
    List<AnimalCategory> GetAll();
    List<AnimalCategory> GetAll(string filter);
    void Update(AnimalCategory category);
    void Delete(int id);
    AnimalCategory? GetById(int id);
}