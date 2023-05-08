using System.ComponentModel.DataAnnotations;

namespace PetShop.Models;

public class Food {

    public int FoodId { get; set; }
    
    [Display(Name = "Marca")]
    public string Make { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; }

    [Display(Name = "Categoria")]
    public string Categorie { get; set; }

    [Display(Name = "Informacón")]
    public string Info { get; set; }

    [Display(Name = "Precio")]
    public float Price { get; set; }
}
