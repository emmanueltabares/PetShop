using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShop.ViewModel.ProductViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? Cod { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}