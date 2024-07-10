using PetShop.Models;
using PetShop.Data;
using Microsoft.EntityFrameworkCore;
using PetShop.Interfaces;

class ProductCategoryService : IProductCategoryService
{
    private readonly ApplicationDbContext _productContext;

    public ProductCategoryService(ApplicationDbContext productContext)
    {
        _productContext = productContext;
    }

    public void Create(ProductCategory category)
    {
        _productContext.Add(category);
        _productContext.SaveChanges();
    }

    public void Delete(int id)
    {
        try {
            var category = GetById(id);

            if (category != null){
                _productContext.Remove(category);
                _productContext.SaveChanges();
            }
        } catch {
            throw new DbUpdateException();
        }
    }

    public List<ProductCategory> GetAll()
    {
        var query = from category in _productContext.Category select category;
        return query.ToList();
    }

    public List<ProductCategory> GetAll(string filter)
    {
        var query = from category in _productContext.Category select category;
        if (!string.IsNullOrEmpty(filter)) {
            query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
        }

        return query.ToList();
    }

    public ProductCategory? GetById(int id)
    {
        var query = from category in _productContext.Category select category;
        return query.Include(x=> x.Products).FirstOrDefault(m => m.ProductCategoryId == id);
    }

    public void Update(ProductCategory category)
    {
         _productContext.Update(category);
        _productContext.SaveChanges();
    }
}