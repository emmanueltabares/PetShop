using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetShop.Models;

namespace PetShop.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    [Display(Name = "Apellido")]
    public required string LastName { get; set; }

    [Display(Name = "Email")]
    public required string Email { get; set; }

    [Display(Name = "Contraseña")]
    public required string Password { get; set; }
    public required int RoleID { get; set; }
    public virtual List<Order> Orders { get; set; }
}