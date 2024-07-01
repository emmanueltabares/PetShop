using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models;

public class AnimalCategory {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnimalCategoryId { get; set; }

    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    public virtual List<Product> Products { get; set; }
}