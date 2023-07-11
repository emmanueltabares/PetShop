using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetShop.Models;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductService _productService;

        public CartController(
            IMemoryCache cache,
            UserManager<IdentityUser> userManager,
            IProductService productService
        )
        {
            _cache = cache;
            _userManager = userManager;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {

            var userName = User.Identity!.Name!;
            var cartModel = new CartViewModel();

            List<CartProduct> cart;

            if(userName != null) {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return View();

                if (_cache.TryGetValue($"Cart_{user.Id}", out cart!))
                {
                    var value = _cache.Get($"Cart_{user.Id}") as List<CartProduct>;
                    cartModel.CartProducts = cart;
                    return View(cartModel);
                }
                return View(cartModel);
            }

            return View(cartModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {

            var userName = User.Identity!.Name;

            if(userName != null) {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return View();

                var product = _productService.GetById(productId);
                if (product == null) return View();

                List<CartProduct> cart;
                if (!_cache.TryGetValue($"Cart_{user.Id}", out cart!))
                {
                    cart = new List<CartProduct>();
                    _cache.Set($"Cart_{user.Id}", cart);
                }

                var cartProduct = new CartProduct();
                cartProduct.product = product;
                cartProduct.quantity = quantity;

                if(cart.Count > 0) {

                    CartProduct productMatch = cart.Find(p => p.product.Id == product.Id);

                    if(productMatch != null) {

                        productMatch.quantity += cartProduct.quantity;

                        _cache.Set($"Cart_${user.Id}", cart);

                        TempData["ProductoAgregado"] = true;
                        return RedirectToAction("Index", "Product");
                    }

                    cart!.Add(cartProduct);
                    _cache.Set($"Cart_{user.Id}", cart);   

                    TempData["ProductoAgregado"] = true;
                    return RedirectToAction("Index", "Product"); 
                } 

                cart!.Add(cartProduct);
                _cache.Set($"Cart_{user.Id}", cart);  

                TempData["ProductoAgregado"] = true;
                return RedirectToAction("Index", "Product");
            }

            TempData["ProductoAgregado"] = true;
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId) {

            var userName = User.Identity!.Name;
            var cartModel = new CartViewModel();

            List<CartProduct> cart;

            if(userName != null) {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return View();

                if (_cache.TryGetValue($"Cart_{user.Id}", out cart!))
                {
                    var value = _cache.Get($"Cart_{user.Id}") as List<CartProduct>;

                    CartProduct productMatch = cart.Find(p => p.product.Id == productId)!;
                    if(productMatch != null) {
                        cart.RemoveAll(p => p.product.Id == productId);
                    }

                    cartModel.CartProducts = cart;
                    // return View("Index");
                    return RedirectToAction("Index", "Cart");
                }
                // return View(cartModel);
                return RedirectToAction("Index", "Cart");
            }

            // return View(cartModel);
            return RedirectToAction("Index", "Cart");
        }

         public async Task<IActionResult> UpdateQuantity(int productId, int quantity) {

            var userName = User.Identity!.Name;
            var cartModel = new CartViewModel();

            List<CartProduct> cart;

            if(userName != null) {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null) return View();

                if (_cache.TryGetValue($"Cart_{user.Id}", out cart!))
                {
                    var value = _cache.Get($"Cart_{user.Id}") as List<CartProduct>;

                    CartProduct productMatch = cart.Find(p => p.product.Id == productId)!;
                    if(productMatch != null) {

                        if(productMatch.product.Stock <= quantity) {
                            productMatch.quantity = quantity;
                            _cache.Set($"Cart_${user.Id}", cart);
                            return RedirectToAction("Index", "Cart");
                        } 

                        return RedirectToAction("Index", "Cart");
                    }
                }
                return RedirectToAction("Index", "Cart");
            }

            return RedirectToAction("Index", "Cart");
        }
    
    }
}
