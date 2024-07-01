using PetShop.Models;
namespace PetShop.ViewModel.ProductCategoryViewModels;

public class ProductCategoryListViewModel
{
    public List<ProductCategory> Categories { get; set; } = new List<ProductCategory>();

    public string? Filter { get; set; }

}