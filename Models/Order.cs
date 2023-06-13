using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetShop.Models;

public class Order {

    public int Id { get; set; }

    [Display(Name = "Producto")]
    public string producto { get; set; }

    [Display(Name = "Cantidad")]
    public int quantity { get; set; }

}
