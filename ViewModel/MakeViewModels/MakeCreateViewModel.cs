using System.ComponentModel.DataAnnotations;
using PetShop.Models;
namespace PetShop.ViewModel.MakeViewModels;

public class MakeCreateViewModel
{
    public int MakeId { get; set; }

    [Required(ErrorMessage = "El nombre de la marca es requerido")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre de la marca debe tener entre 3 y 20 caracteres")]
    public string Name { get; set; }

    public string? Filter { get; set; }
}
