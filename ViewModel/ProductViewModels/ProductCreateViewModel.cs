using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre del producto debe tener entre 3 y 80 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Código del producto")]
        [Range(1, 10, ErrorMessage = "El código debe estar entre 0 y 10 caracteres")]
        public int? Cod { get; set; }

        [Display(Name = "Descripción del producto")]
        [StringLength(100, ErrorMessage = "La descripción del producto debe tener menos de 100 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio del producto es requerido")]
        [Range(1, 1000000, ErrorMessage = "El precio del producto debe estar entre 0 y 1000000")]
        public float Price { get; set; }

        [Required(ErrorMessage = "La cantidad de stock del producto es requerida")]
        [Range(1, 1000000, ErrorMessage = "La cantidad de stock del producto debe estar entre 0 y 1000000")]
        public int Stock { get; set; }

        [Display(Name = "Categoría del producto")]
        [Required(ErrorMessage = "La categoría del producto es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La categoría del producto es requerida")]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Categoría de animal del producto")]
        [Required(ErrorMessage = "La categoría de animal del producto es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La categoría de animal del producto es requerida")]
        public int AnimalCategoryId { get; set; }

        [Display(Name = "Marca del producto")]
        [Required(ErrorMessage = "La marca del producto es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La marca del producto es requerida")]
        public int MakeId { get; set; }
        public List<SelectListItem> AnimalCategories { get; set; }
        public List<SelectListItem> Makes { get; set; }
        public List<SelectListItem> ProductCategories { get; set; }
    }
}