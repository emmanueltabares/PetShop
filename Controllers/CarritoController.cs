using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PetShop.Data;
using PetShop.Models;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductService _productService;

        public CarritoController(IMemoryCache cache, UserManager<IdentityUser> userManager, IProductService productService)
        {
            _cache = cache;
            _userManager = userManager;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {

            var userName = User.Identity.Name;
            var cartModel = new CartViewModel();
            List<Product> carrito;

            if(userName != null) {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return View();

                Console.WriteLine(user.Id);

                if (_cache.TryGetValue($"Cart_{user.Id}", out carrito))
                {
                    var value = _cache.Get($"Cart_{user.Id}");
                    cartModel.Products = carrito;
                    return View(cartModel);
                }
                return View(cartModel);
            }

            return View(cartModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            Console.WriteLine("add to cart");
            
            var userName = User.Identity.Name;

            if(userName != null) {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return View();

                Console.WriteLine(user.Id);
                // Obtener el producto seleccionado por su ID desde una fuente de datos
                var product = _productService.GetById(productId);
                if (product == null) return View();

                // Obtener o crear la lista de productos en el carrito desde la caché
                List<Product> carrito;
                if (!_cache.TryGetValue($"Cart_{user.Id}", out carrito))
                {
                    carrito = new List<Product>();
                    _cache.Set($"Cart_{user.Id}", carrito);
                }

                // Agregar el producto al carrito
                carrito.Add(product);

                _cache.Set($"Cart_{user.Id}", carrito);

                Console.WriteLine(_cache.Get($"Cart_{user.Id}"));

                return RedirectToAction("Index", "Product");
            }


            // Redirigir a la acción Index del controlador Carrito para mostrar el contenido del carrito
            return RedirectToAction("Index", "Product");
        }


    }
}
