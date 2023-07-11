using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;
using PetShop.ViewModel;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace PetShop.Controllers;

public class OrderController : Controller {

    private readonly IOrderService _orderService;
    private readonly IProductService _productService;


    public OrderController(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    public IActionResult Index()
    {

        // if(User.Identity.IsAuthenticated )
        var model = new OrderViewModel();
        model.orders = _orderService.GetAll();
        
        return View(model);
    } 

    [HttpPost]
    public IActionResult CreateOrder(List<CartProduct> CartProducts)
    {

        if(CartProducts != null) {
            // List<CartProduct> cartProducts = JsonSerializer.Deserialize<List<CartProduct>>(cart);
            Console.WriteLine("Llgue");

            var order = new Order();
            order.Date = DateTime.Now;
            // order.Total = cartProducts.
            _orderService.Create(order);    

            return RedirectToAction("Index");

        }


        


        // _orderService.Create(order);
        return RedirectToAction("Index");
        
    }

}