using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? Cod { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public int ProductCategoryId { get; set; }
        public int AnimalCategoryId { get; set; }
        public int MakeId { get; set; }
        public List<SelectListItem> AnimalCategories { get; set; }
        public List<SelectListItem> Makes { get; set; }
        public List<SelectListItem> ProductCategories { get; set; }
    }
}