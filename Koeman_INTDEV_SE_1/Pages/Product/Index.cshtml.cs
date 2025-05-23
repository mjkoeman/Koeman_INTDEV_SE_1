using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;
using System.Text.Json;

namespace Koeman_INTDEV_SE_1.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 _context;

        public IndexModel(Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 context)
        {
            _context = context;
        }

        public IList<Models.Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Producten.ToListAsync();
        }
        public IActionResult OnPostAddToCart(int productId)
        {
            var cart = HttpContext.Session.GetString("Cart");
            List<int> cartItems = string.IsNullOrEmpty(cart)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(cart);

            cartItems.Add(productId);

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
            return RedirectToPage();
        }
    }
}
