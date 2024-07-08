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
using Microsoft.AspNetCore.Authorization;

namespace PetShop.Controllers;

[Authorize (Roles = "Administrador, Ventas, Logistica")]
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

    public IActionResult Create(int? id)
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

        if(id != null) {
            var currentOrder = _orderService.GetById(id.Value);
            if(currentOrder == null) return NotFound();

            model.OrderProducts = currentOrder.OrderDetails.Select(x => new OrderProductViewModel {
                ProductId = x.ProductId,
                Name = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity
            }).ToList();
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

    public IActionResult RemoveProductOnEditOrder(OrderEditProductViewModel model, int productId)
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

        model.OrderId = model.OrderId;

        return View("Edit", model);
    }

    public IActionResult AddProductOnEditOrder(OrderEditProductViewModel model)
    {
        var product = _productService.GetById(model.SelectedProductId);

        if (product != null)
        {
            var existingProduct = model.OrderProducts.FirstOrDefault(p => p.ProductId == model.SelectedProductId);
            if (existingProduct != null)
            {
                existingProduct.Quantity += model.Quantity;
            }
            else
            {
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

        model.OrderId = model.OrderId;

        return View("Edit", model);
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
                OrderDate = DateTime.Now.ToString("dd/MM/yyyy"),
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

    public IActionResult Delete(int id)
    {
        var order = _orderService.GetById(id);
        if(order == null) return NotFound();

        if(order.ShippingDate != null) {
            TempData["ErrorMessage"] = "No es posible eliminar una orden de compra despachada.";
            return RedirectToAction("Index");
        }

        var model = new OrderDeleteViewModel(){
            OrderId = order.OrderId
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(OrderDeleteViewModel model)
    {
        var order = _orderService.GetById(model.OrderId);
        if(order == null) return NotFound();

        try {
            _orderService.Delete(order.OrderId);
            TempData["SuccessMessage"] = "Orden de compra eliminada correctamente.";
            return RedirectToAction("Index");
        }
        catch {
            TempData["ErrorMessage"] = "No se puede eliminar la orden de compra.";
            return RedirectToAction("Delete", new { id = model.OrderId });
        }
    }

    public IActionResult Edit(int id)
    {
        try {
            var model = new OrderEditProductViewModel
            {
                OrderProducts = new List<OrderProductViewModel>()
            };

            var products = _productService.GetAll();
            foreach(var product in products) {
                model.AvailableProducts.Add(
                    new SelectListItem { Value = product.ProductId.ToString(), Text = product.Name }
                );
            }

            if(id != null) {
                var currentOrder = _orderService.GetById(id);
                if(currentOrder == null) return NotFound();

                if(currentOrder.ShippingDate != null) {
                    TempData["ErrorMessage"] = "No es posible editar una orden de compra despachada.";
                    return RedirectToAction("Index");
                }

                model.OrderProducts = currentOrder.OrderDetails.Select(x => new OrderProductViewModel {
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList();

                model.OrderId = currentOrder.OrderId;
            }

            return View(model);

        } catch (Exception ex) {
            TempData["ErrorMessage"] = "No se puede editar la orden de compra." + ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Edit(OrderCreateViewModel model, int orderId)
    {
        try {
            var order = _orderService.GetById(orderId);
            if(order == null) return NotFound();

            if(ModelState.IsValid) {

                try {
                    _orderDetailService.Remove(order.OrderId);

                    var totalProductsInOrder = model.OrderProducts.Sum(x => x.Quantity);
                    var totalPrice = model.OrderProducts.Sum(x => x.Price * x.Quantity);

                    order.TotalProducts = totalProductsInOrder;
                    order.TotalPrice = (decimal)totalPrice;

                    _orderService.Update(order);

                    foreach(var product in model.OrderProducts) {
                        var orderProductDetail = new OrderDetail() {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            Quantity = product.Quantity
                        };

                        _orderDetailService.Create(orderProductDetail);
                    }

                    TempData["SuccessMessage"] = "Orden de compra actualizada correctamente.";
                    return RedirectToAction("Index");
                } catch {
                    TempData["ErrorMessage"] = "No se puede actualizar la orden de compra.";
                    return RedirectToAction("Edit", new { id = orderId });
                }
            }
            return View(model);
        } catch (Exception ex) {
            TempData["ErrorMessage"] = "No se puede editar la orden de compra." + ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [ActionName("Dispatch")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Logistica")]
    public IActionResult DispatchOrder(int orderId)
    {
       try {
            if(orderId == null) return RedirectToAction(nameof(Index));   
            var order = _orderService.GetById(orderId);
            if(order == null) return NotFound();

            foreach(var orderDetail in order.OrderDetails) {
                var product = _productService.GetById(orderDetail.ProductId);
                if(product.Stock < orderDetail.Quantity) {
                    TempData["ErrorMessage"] = "No se puede despachar la orden de compra. Stock Insuficiente";
                    return RedirectToAction("Index");
                }

                product.Stock -= orderDetail.Quantity;
                _productService.Update(product);
            }

                _orderService.Dispatch(order.OrderId);
                TempData["SuccessMessage"] = "Orden de compra despachada correctamente.";
                return RedirectToAction("Index");
        } catch {
            TempData["ErrorMessage"] = "No se puede despachar la orden de compra.";
            return RedirectToAction("Index");
        }
    }
}