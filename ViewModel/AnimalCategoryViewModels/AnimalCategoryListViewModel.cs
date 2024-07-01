using PetShop.Models;
namespace PetShop.ViewModel.AnimalCategoryViewModels;

public class AnimalCategoryListViewModel
{
    public List<AnimalCategory> Categories { get; set; } = new List<AnimalCategory>();

    public string? Filter { get; set; }

}