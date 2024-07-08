using System.ComponentModel.DataAnnotations;

namespace PetShop.ViewModel.RoleViewModels;

public class RoleCreateViewModel
{
    [Required(ErrorMessage = "El nombre del rol es requerido")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre del rol debe tener entre 3 y 20 caracteres")]
    public string Name { get; set; }
}