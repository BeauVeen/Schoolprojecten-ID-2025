using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using KE03_INTDEV_SE_1_Base.Extensions;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductenModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public List<Product> Products { get; set; } = new();

        public ProductenModel(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Cart")
                ?? new Dictionary<int, int>();

            if (cart.ContainsKey(productId))
                cart[productId]++;
            else
                cart[productId] = 1;

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            var addedProduct = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (addedProduct != null)
                TempData["SuccessMessage"] = $"'{addedProduct.Name}' is toegevoegd aan je winkelmand.";
            
            return RedirectToPage();
        }
    }
}