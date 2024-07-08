using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.ProductViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre del producto debe tener entre 3 y 20 caracteres")]
        public string Name { get; set; }
        public int? Cod { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio del producto es requerido")]
        [Range(0, 1000000, ErrorMessage = "El precio del producto debe estar entre 0 y 1000000")]
        public float Price { get; set; }

        [Required(ErrorMessage = "La cantidad de stock del producto es requerida")]
        [Range(0, 1000000, ErrorMessage = "La cantidad de stock del producto debe estar entre 0 y 1000000")]
        public int Stock { get; set; }
    }
}