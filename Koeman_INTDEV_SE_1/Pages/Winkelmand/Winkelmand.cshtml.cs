using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Koeman_INTDEV_SE_1.Pages.Winkelmand
{
    public class WinkelmandModel : PageModel
    {
        private readonly DbContextINTDEV1 _context;

        public WinkelmandModel(DbContextINTDEV1 context)
        {
            _context = context;
        }

        public class WinkelmandItem
        {
            public Koeman_INTDEV_SE_1.Models.Product Product { get; set; }
            public int Aantal { get; set; }
        }

        public List<WinkelmandItem> WinkelmandProducten { get; set; } = new();

        public decimal TotaalPrijs { get; set; }

        public async Task OnGetAsync()
        {
            var cartJson = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartJson))
            {
                WinkelmandProducten = new List<WinkelmandItem>();
                TotaalPrijs = 0;
                return;
            }

            var productIds = JsonSerializer.Deserialize<List<int>>(cartJson);

            var grouped = productIds
                .GroupBy(id => id)
                .Select(g => new { ProductId = g.Key, Aantal = g.Count() })
                .ToList();

            var producten = await _context.Producten
                .Where(p => grouped.Select(g => g.ProductId).Contains(p.ProductId))
                .ToListAsync();

            WinkelmandProducten = grouped
                .Select(g => new WinkelmandItem
                {
                    Product = producten.First(p => p.ProductId == g.ProductId),
                    Aantal = g.Aantal
                })
                .ToList();

            TotaalPrijs = WinkelmandProducten.Sum(w => w.Product.Prijs * w.Aantal);
        }
    }
}