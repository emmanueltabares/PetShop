using PetShop.Models;
using PetShop.Data;
using Microsoft.EntityFrameworkCore;
using PetShop.Interfaces;

class AnimalCategoryService : IAnimalCategoryService
{
    private readonly ApplicationDbContext _productContext;

    public AnimalCategoryService(ApplicationDbContext productContext)
    {
        _productContext = productContext;
    }

    List<AnimalCategory> IAnimalCategoryService.GetAll()
    {
        var query = from category in _productContext.AnimalCategory select category;
        return query.ToList();
    }

    List<AnimalCategory> IAnimalCategoryService.GetAll(string filter)
    {
        var query = from category in _productContext.AnimalCategory select category;
        if (!string.IsNullOrEmpty(filter)) {
            query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }

        return query.ToList();
    }

    public AnimalCategory GetById(int id)
    {
        var query = from category in _productContext.AnimalCategory select category;
        return query.FirstOrDefault(m => m.AnimalCategoryId == id);
    }

    void IAnimalCategoryService.Create(AnimalCategory anomalyCategory)
    {
        _productContext.Add(anomalyCategory);
        _productContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var obj = GetById(id);

        if (obj != null){
            _productContext.Remove(obj);
            _productContext.SaveChanges();
        }
    }

    public void Update(AnimalCategory category)
    {
        _productContext.Update(category);
        _productContext.SaveChanges();
    }
}