using PetShop.Models;

public interface ICategoryService
{
    void Create(Category category);
    List<Category> GetAll();
    List<Category> GetAll(string filter);
    void Update(Category category);
    void Delete(int id);
    Category? GetById(int id);
}