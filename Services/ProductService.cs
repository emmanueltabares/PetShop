using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

class ProductService : IProductService {

    private readonly ApplicationDbContext _productContext;

    public ProductService(ApplicationDbContext productContext)
    {
        _productContext = productContext;
    }

    public void Create(Product product)
    {
        _productContext.Add(product);
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

    public List<Product> GetAll(string filter)
    {
        var query = from product in _productContext.Product select product;

        if (!string.IsNullOrEmpty(filter)) {
            query = query.Where(x => 
                    x.Name.Contains(filter) ||
                    x.ProductCategory.Name.Contains(filter) ||
                    x.AnimalCategory.Name.Contains(filter) ||
                    x.Make.Name.Contains(filter)
                )
                .Include(p => p.ProductCategory)
                .Include(p => p.AnimalCategory)
                .Include(p => p.Make);
        }

        return query.ToList();
    }

    public List<Product> GetAll()
    {

        List<Product> products = (from product in _productContext.Product select product)
            .Include(p => p.ProductCategory)
            .Include(p => p.AnimalCategory)
            .Include(p => p.Make)
            .ToList();
            
        return products;
    }

    public Product? GetById(int id)
    {
        var query = from product in _productContext.Product select product;
        return query.FirstOrDefault(m => m.ProductId == id);
    }

    public void Update(Product obj)
    {
        _productContext.Update(obj);
        _productContext.SaveChanges();
    }
}