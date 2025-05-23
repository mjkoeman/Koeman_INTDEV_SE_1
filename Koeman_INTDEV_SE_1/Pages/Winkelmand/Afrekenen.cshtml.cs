using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Koeman_INTDEV_SE_1.Pages.Winkelmand
{
    public class AfrekenenModel : PageModel
    {
        private readonly DbContextINTDEV1 _context;

        public AfrekenenModel(DbContextINTDEV1 context)
        {
            _context = context;
        }

        public List<WinkelmandItem> WinkelmandProducten { get; set; } = new();

        public decimal TotaalPrijs { get; set; }

        [BindProperty]
        public Koeman_INTDEV_SE_1.Models.Bestelling Bestelling { get; set; } = new Koeman_INTDEV_SE_1.Models.Bestelling();

        public class WinkelmandItem
        {
            public Koeman_INTDEV_SE_1.Models.Product Product { get; set; }
            public int Aantal { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                ModelState.AddModelError(string.Empty, "Winkelmand is leeg.");
                return Page();
            }

            var productIds = JsonSerializer.Deserialize<List<int>>(cartJson);
            var grouped = productIds
                .GroupBy(id => id)
                .Select(g => new { ProductId = g.Key, Aantal = g.Count() })
                .ToList();

            var producten = await _context.Producten
                .Where(p => grouped.Select(g => g.ProductId).Contains(p.ProductId))
                .ToListAsync();

            decimal totaal = 0;

            var bestellingProducten = new List<BestellingProduct>();

            foreach (var group in grouped)
            {
                var product = producten.First(p => p.ProductId == group.ProductId);
                totaal += product.Prijs * group.Aantal;

                bestellingProducten.Add(new BestellingProduct
                {
                    ProductId = product.ProductId,
                    Aantal = group.Aantal
                });
            }

            Bestelling.Besteldatum = DateTime.Now;
            Bestelling.Totaalbedrag = totaal;
            Bestelling.BestellingProducten = bestellingProducten;

            _context.Bestellingen.Add(Bestelling);
            await _context.SaveChangesAsync();

            // Leeg de winkelmand
            HttpContext.Session.Remove("Cart");

            return RedirectToPage("/Bestelling/Bevestiging"); // Maak deze pagina als je wilt
        }
    }
}

