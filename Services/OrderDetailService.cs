using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

class OrderDetailService : IOrderDetailService {

  private readonly ApplicationDbContext _context;

  public OrderDetailService(ApplicationDbContext context)
  {
    _context = context;
  }

  public void Create(OrderDetail obj)
  {
      _context.Add(obj);
      _context.SaveChanges();
  }

  public void GetById(int id)
  {
      throw new NotImplementedException();
  }

  public void Remove(int id)
  {
    var orderDetails = _context.OrderDetail.Where(od => od.OrderId == id).ToList();

    if (orderDetails.Any()) {
        _context.OrderDetail.RemoveRange(orderDetails);
        _context.SaveChanges();
    }
  }
}