using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.ViewModel;

public class MakeService : IMakeService
{

    private readonly ApplicationDbContext _context;

    public MakeService(ApplicationDbContext productContext)
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
         var make = GetById(id);

        if (make != null){
            _context.Remove(make);
            _context.SaveChanges();
        }
    }

    public List<Make> GetAll()
    {
        var query = from make in _context.Make select make;
        return query.ToList();
    }

    public List<Make> GetAll(string filter)
    {
        var query = from make in _context.Make select make;
        if (!string.IsNullOrEmpty(filter)) {
            query = query.Where(x => x.Name.Contains(filter));
        }

        return query.ToList();
    }

    public Make GetById(int id)
    {
        var query = from make in _context.Make select make;
        return query.FirstOrDefault(m => m.MakeId == id);
    }

    public void Update(Make make)
    {
        _context.Update(make);
        _context.SaveChanges();
    }
}