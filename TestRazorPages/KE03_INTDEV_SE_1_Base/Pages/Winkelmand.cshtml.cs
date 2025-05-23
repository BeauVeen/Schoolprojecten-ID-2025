using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using KE03_INTDEV_SE_1_Base.Extensions;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class WinkelmandModel : PageModel
    {
        private readonly MatrixIncDbContext _context;
        public List<(Product Product, int Aantal)> WinkelmandItems { get; set; } = new();

        public decimal TotaalPrijs => WinkelmandItems.Sum(i => i.Product.Price * i.Aantal);

        public WinkelmandModel(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Cart")
                ?? new Dictionary<int, int>();

            var productIds = cart.Keys.ToList();
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();

            WinkelmandItems = products
                .Select(p => (p, cart[p.Id]))
                .ToList();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Cart")
                ?? new Dictionary<int, int>();

            if (cart.ContainsKey(productId))
            {
                cart.Remove(productId);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostIncreaseQuantity(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Cart")
                ?? new Dictionary<int, int>();

            if (cart.ContainsKey(productId))
                cart[productId]++;
            else
                cart[productId] = 1;

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage();
        }

        public IActionResult OnPostDecreaseQuantity(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Cart")
                ?? new Dictionary<int, int>();

            if (cart.ContainsKey(productId))
            {
                cart[productId]--;
                if (cart[productId] <= 0)
                    cart.Remove(productId);
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage();
        }

        public IActionResult OnPostPlaceOrder()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Cart");
            if (cart == null || !cart.Any())
            {
                TempData["ErrorMessage"] = "Je winkelmand is leeg";
                return RedirectToPage();
            }

            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "Gebruiker niet ingelogd.";
                return RedirectToPage();
            }

            var username = User.Identity.Name;

            var customer = _context.Customers.FirstOrDefault(c => c.Name == username);

            if (customer == null)
            {
                TempData["ErrorMessage"] = "Gebruiker niet gevonden.";
                return RedirectToPage();
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var entry in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == entry.Key);
                if (product == null) continue;

                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = entry.Value
                });
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            HttpContext.Session.Remove("Cart");
            TempData["SuccessMessage"] = "Je bestelling is geplaatst.";

            return RedirectToPage("/Bestellingen");
        }
    }
}
