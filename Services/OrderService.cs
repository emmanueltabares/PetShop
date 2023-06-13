using PetShop.Data;
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

}