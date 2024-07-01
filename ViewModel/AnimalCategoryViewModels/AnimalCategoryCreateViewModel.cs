using PetShop.Models;

namespace PetShop.ViewModel.AnimalCategoryViewModels;

public class AnimalCategoryCreateViewModel
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }

    public string? Filter { get; set; }
}
