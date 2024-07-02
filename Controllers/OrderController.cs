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
using PetShop.Interfaces;
using PetShop.ViewModel.OrderViewModels;
using Microsoft.AspNetCore.Identity;

namespace PetShop.Controllers;

public class OrderController : Controller {

    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IOrderDetailService _orderDetailService;
    private readonly UserManager<IdentityUser> _userManager;


    public OrderController(
        IOrderService orderService,
        IProductService productService,
        IOrderDetailService orderDetailService,
        UserManager<IdentityUser> userManager
    )
    {
        _orderService = orderService;
        _productService = productService;
        _orderDetailService = orderDetailService;
        _userManager = userManager;
    }

    public IActionResult Index(string? filter)
    {

        var model = new OrderListViewModel();

        if(!string.IsNullOrEmpty(filter)) {
            model.Orders = _orderService.GetAll(filter);
        } else {
            model.Orders = _orderService.GetAll();
        }

        return View(model);
    } 

    public IActionResult Create()
    {
        var model = new OrderAddProductViewModel
        {
            OrderProducts = new List<OrderProductViewModel>()
        };

        var products = _productService.GetAll();
        foreach(var product in products) {
            model.AvailableProducts.Add(
                new SelectListItem { Value = product.ProductId.ToString(), Text = product.Name }
            );
        }

        return View(model); 
    }

    public IActionResult AddProduct(OrderAddProductViewModel model)
        {
            var product = _productService.GetById(model.SelectedProductId);

            if (product != null) {
                var existingProduct = model.OrderProducts.FirstOrDefault(p => p.ProductId == model.SelectedProductId);
                if (existingProduct != null) {
                    existingProduct.Quantity += model.Quantity;
                }
                else {
                    model.OrderProducts.Add(new OrderProductViewModel
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = model.Quantity
                    });
                }
            }

            model.AvailableProducts = _productService.GetAll()
                .Select(p => new SelectListItem { Value = p.ProductId.ToString(), Text = p.Name })
                .ToList();

            return View("Create", model);
        }

    public IActionResult RemoveProduct(OrderAddProductViewModel model, int productId)
    {
        var product = _productService.GetById(productId);

        if (product != null)
        {
            var existingProduct = model.OrderProducts.FirstOrDefault(p => p.ProductId == productId);
            if (existingProduct != null)
            {
                model.OrderProducts.Remove(existingProduct);
            }
        }

        model.AvailableProducts = _productService.GetAll()
            .Select(p => new SelectListItem { Value = p.ProductId.ToString(), Text = p.Name })
            .ToList();

        return View("Create", model);
    }

    [HttpPost]
    public IActionResult Create(OrderCreateViewModel orderModel)
    {

        if(ModelState.IsValid) {

            var loggedInUser = _userManager.GetUserAsync(User);
            if(loggedInUser.Result == null) return NotFound();

            var totalProductsInOrder = orderModel.OrderProducts.Sum(x => x.Quantity);
            var totalPrice = orderModel.OrderProducts.Sum(x => x.Price * x.Quantity);

            var order = new Order() {
                UserId = loggedInUser.Result.Id,
                OrderDate = DateTime.Now.ToString("dd/MM/yyyy"), // new DateTime().GetDateTimeFormats().ToString(),
                TotalProducts = totalProductsInOrder,
                TotalPrice = (decimal)totalPrice,
            };
            _orderService.Create(order);

            foreach(var product in orderModel.OrderProducts) {
                var orderProductDetail = new OrderDetail() {
                    OrderId = order.OrderId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                };

                _orderDetailService.Create(orderProductDetail);
            }

            TempData["SuccessMessage"] = "Orden de compra agregada correctamente.";
            return RedirectToAction("Index");
        }
        return View(orderModel);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var order = _orderService.GetById(id);
        if(order == null) return NotFound();

        var model = new OrderDetailsViewModel(){};
        model.OrderProducts = order.OrderDetails.Select(x => new OrderProductViewModel {
            ProductId = x.ProductId,
            Name = x.Product.Name,
            Price = x.Product.Price,
            Quantity = x.Quantity
        }).ToList();

        model.UserName = order.User.UserName;
        return View(model);
    }
}