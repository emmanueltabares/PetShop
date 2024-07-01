using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetShop.Models;

namespace PetShop.Models;

public class ProductCategory {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductCategoryId { get; set; }

    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    public virtual List<Product> Products { get; set; }
}