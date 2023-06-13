using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;

class ProductService : IProductService {

  private readonly ProductContext _productContext;

  public ProductService(ProductContext productContext)
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
      query = query.Where(x => x.Name.Contains(filter));
    }

    return query.ToList();
  }

  public List<Product> GetAll()
  {
    // .Select((fields => field.Name))
    List<Product> products = (from product in _productContext.Product select product).Include(x => x.Category).ToList();
    return products;
  }

  public Product? GetById(int id)
  {
    var query = from product in _productContext.Product select product;
    return query.Include(x=> x.Category).FirstOrDefault(m => m.Id == id);
  }

  public void Update(Product obj)
  {
    _productContext.Update(obj);
    _productContext.SaveChanges();
  }
}