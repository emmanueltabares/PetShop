using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

class OrderService : IOrderService {

  private readonly ProductContext _context;

  public OrderService(ProductContext context)
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
      query = query.Where(x => x.OrderDate.ToString().Contains(filter));
    }

    return query.ToList();
  }
}