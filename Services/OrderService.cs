using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

class OrderService : IOrderService {

  private readonly ApplicationDbContext _context;

  public OrderService(ApplicationDbContext context)
  {
    _context = context;
  }

  public void Create(Order order)
  {
    _context.Add(order);
    _context.SaveChanges();
  }

  public List<Order> GetAll()
  {
    var query = from order in _context.Order select order;
    return query.ToList();
  }

  public List<Order> GetAll(string filter)
  {
    var query = from order in _context.Order select order;
    if (!string.IsNullOrEmpty(filter)) {
      query = query.Where(
          x => x.OrderId.ToString().Contains(filter)
          || x.OrderDate.Contains(filter)
          || x.ShippingDate.Contains(filter)
        );
    }

    return query.ToList();
  }

  public Order GetById(int id)
  {
    var query = from order in _context.Order select order;
    return query
      .Include(x => x.User)
      .Include(x => x.OrderDetails)
      .ThenInclude(od => od.Product)
      .FirstOrDefault(x => x.OrderId == id);
  }

  public void Delete (int id)
  {
    var obj = GetById(id);

    if (obj != null) {
        _context.Remove(obj);
        _context.SaveChanges();
    }
  } 

  public void Update(Order order)
  {
    _context.Update(order);
    _context.SaveChanges();
  }

  public void Dispatch(int id)
  {
    try {
      var order = GetById(id) ?? throw new Exception();
      order.ShippingDate = DateTime.Now.ToString("dd/MM/yyyy");
      _context.Update(order);
      _context.SaveChanges();

    } catch {
      throw new Exception();
    }
  }

}