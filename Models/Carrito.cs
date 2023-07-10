using System.ComponentModel.DataAnnotations;
using PetShop.Models;

namespace PetShop.Models;

public class Carrito {

    public int Id { get; set; }
    public string UserId { get; set; }

    public virtual List<Product> Products { get; set; }
}