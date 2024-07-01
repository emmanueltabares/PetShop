using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetShop.Models;

public class Make {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MakeId { get; set; }

    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    [Display(Name = "Descripción")]
    public string Description { get; set; }

    public virtual List<Product> Products { get; set; }

}
