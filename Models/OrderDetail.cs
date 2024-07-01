
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }

}