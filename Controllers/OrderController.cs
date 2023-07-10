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

    public IActionResult Create()
    {

        List<Product> products = _productService.GetAll();
        var model = new OrderCreateViewModel();

        model.products = products;
        model.quantity = 0;
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderService.Create(order);
            return RedirectToAction("Detalle", new { id = order.Id });
        }

        return View(order);
    }

}