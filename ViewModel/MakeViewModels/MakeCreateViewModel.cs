using PetShop.Models;
namespace PetShop.ViewModel.MakeViewModels;

public class MakeCreateViewModel
{
    public int MakeId { get; set; }
    public string Name { get; set; }

    public string? Filter { get; set; }
}
