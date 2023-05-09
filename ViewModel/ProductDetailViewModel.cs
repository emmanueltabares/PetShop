using PetShop.Models;

namespace PetShop.ViewModel;

public class ProductDetailViewModel
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }

     public List<Category> Categories { get; set; }
 
}