using System.ComponentModel.DataAnnotations;
using PetShop.Models;

namespace PetShop.Models;

public class Category {

    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; }

    public virtual List<Product> Products { get; set; }
}