using System.ComponentModel.DataAnnotations;

namespace PetShop.Models;

public class Product {

    public int Id { get; set; }
    
    [Display(Name = "Marca")]
    public string Make { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; }

    [Display(Name = "Precio")]
    public float Price { get; set; }

    [Display(Name = "Stock")]
    public int Stock { get; set; }

    [Display(Name = "Categoría")]
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
}
