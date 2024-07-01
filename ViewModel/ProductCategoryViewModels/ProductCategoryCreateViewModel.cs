using PetShop.Models;

namespace PetShop.ViewModel.ProductCategoryViewModels;

public class ProductCategoryCreateViewModel
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }

    public string? Filter { get; set; }
}
