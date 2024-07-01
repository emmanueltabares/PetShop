using PetShop.Models;
namespace PetShop.ViewModel.MakeViewModels;

public class MakeListViewModel
{
    public List<Make> Makes { get; set; } = new List<Make>();

    public string? Filter { get; set; }

}