using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.ViewModel;

public class MakeService : IMakeService
{

    private readonly ProductContext _context;

    public MakeService(ProductContext productContext)
    {
        _context = productContext;
    }
    public void Create(Make obj)
    {
        _context.Make.Add(obj);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Make> GetAll()
    {
        var query = from make in _context.Make select make;
        return query.ToList();
    }

    public List<Make> GetAll(string filter)
    {
        throw new System.NotImplementedException();
    }

    public Make GetById(int id)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Make obj)
    {
        throw new System.NotImplementedException();
    }
}