using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models;

public class Product {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    public int MakeId { get; set; }
    public int ProductCategoryId { get; set; }
    public int AnimalCategoryId { get; set; }
    public required string Name { get; set; }
    public int? Cod { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public virtual ProductCategory ProductCategory { get; set; }
    public virtual AnimalCategory AnimalCategory { get; set; }
    public virtual Make Make { get; set; }
    public virtual List<OrderDetail> OrderDetails { get; set; }
}
