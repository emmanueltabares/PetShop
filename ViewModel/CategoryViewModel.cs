using PetShop.Models;
namespace PetShop.ViewModel;

public class CategoryViewModel {

    public List<Category> Category { get; set; } = new List<Category>();

    public string? Filter { get; set; }

}