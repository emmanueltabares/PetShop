using PetShop.Models;

namespace PetShop.ViewModel;

public class CategoryDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}