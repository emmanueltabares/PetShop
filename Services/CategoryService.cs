using PetShop.Models;
using PetShop.Data;
using Microsoft.EntityFrameworkCore;

class CategoryService : ICategoryService
{
    private readonly ProductContext _productContext;

    public CategoryService(ProductContext productContext)
    {
        _productContext = productContext;
    }
    public void Create(Category category)
    {
        _productContext.Add(category);
        _productContext.SaveChanges();
    }

    public void Delete(int id)
    {
         var category = GetById(id);
    
        if (category != null){
            _productContext.Remove(category);
            _productContext.SaveChanges();
        }
    }

    public List<Category> GetAll()
    {
        var query = from category in _productContext.Category select category;
        return query.Include(x => x.Products).ToList();
    }

    public List<Category> GetAll(string filter)
    {
        var query = from category in _productContext.Category select category;
        if (!string.IsNullOrEmpty(filter)) {
            query = query.Where(x => x.Name.Contains(filter));
        }

        return query.ToList();
    }

    public Category? GetById(int id)
    {
        var query = from category in _productContext.Category select category;
        return query.Include(x=> x.Products).FirstOrDefault(m => m.Id == id);
    }

    public void Update(Category category)
    {
         _productContext.Update(category);
        _productContext.SaveChanges();
    }
}