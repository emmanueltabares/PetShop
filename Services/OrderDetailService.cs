using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

class OrderDetailService : IOrderDetailService {

  private readonly ProductContext _context;

  public OrderDetailService(ProductContext context)
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
}