using System.ComponentModel.DataAnnotations;
using PetShop.Models;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Identity;

namespace PetShop.Models;

public class Order {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public required string UserId { get; set; }

    [Display(Name = "Total de productos")]
    public decimal TotalProducts { get; set; }

    [Display(Name = "Precio total")]
    public decimal TotalPrice { get; set; }

    [Display(Name = "Fecha de la orden")]
    public string OrderDate { get; set; }

    [Display(Name = "Fecha de envío")]
    public string? ShippingDate { get; set; }

    public virtual IdentityUser User { get; set; }

    public virtual List<OrderDetail> OrderDetails { get; set; }

}
