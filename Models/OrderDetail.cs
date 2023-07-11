
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductoId { get; set; }
    public int quantity { get; set; }
    public decimal Price { get; set; }

}