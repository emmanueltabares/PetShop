using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetShop.Models;

public class Order {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }

    public List<OrderDetail> Details { get; set; }

    public bool confirmed { get; set; }
}
