using System.ComponentModel.DataAnnotations;
using PetShop.Models;

namespace PetShop.Models;

public class CartProduct {
    public Product product = new Product();
    public int quantity;
}