using System.ComponentModel.DataAnnotations;
using PetShop.Models;

namespace PetShop.ViewModel.AnimalCategoryViewModels;

public class AnimalCategoryEditViewModel
{
    public int AnimalCategoryId { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es requerido")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre de la categoría debe tener entre 3 y 20 caracteres")]
    public string Name { get; set; }

}
