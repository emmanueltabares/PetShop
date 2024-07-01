using PetShop.Models;
namespace PetShop.Interfaces;

public interface IProductCategoryService
{
    void Create(ProductCategory category);
    List<ProductCategory> GetAll();
    List<ProductCategory> GetAll(string filter);
    void Update(ProductCategory category);
    void Delete(int id);
    ProductCategory? GetById(int id);
}