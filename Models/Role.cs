using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetShop.Models;

namespace PetShop.Models;

public class Role {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }

    [Display(Name = "Nombre")]
    public required string Name { get; set; }
    public virtual List<User> Users { get; set; }
}