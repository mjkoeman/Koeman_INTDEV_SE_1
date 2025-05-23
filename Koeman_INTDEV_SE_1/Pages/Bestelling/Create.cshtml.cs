using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;    


namespace Koeman_INTDEV_SE_1.Pages.Bestelling
{
    public class CreateModel : PageModel
    {
        private readonly Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 _context;

        public CreateModel(Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["KlantId"] = new SelectList(_context.Klanten, "KlantId", "Naam");
            return Page();
        }

        [BindProperty]
        public Koeman_INTDEV_SE_1.Models.Bestelling Bestelling { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var winkelmandJson = HttpContext.Session.GetString("Winkelmand");

            if (string.IsNullOrEmpty(winkelmandJson))
            {
                ModelState.AddModelError(string.Empty, "Je winkelmand is leeg.");
                ViewData["KlantId"] = new SelectList(_context.Klanten, "KlantId", "Naam");
                return Page();
            }

            var productIds = JsonSerializer.Deserialize<List<int>>(winkelmandJson);

            if (!ModelState.IsValid)
            {
                ViewData["KlantId"] = new SelectList(_context.Klanten, "KlantId", "Naam");
                return Page();
            }

            // Bestelling maken
            Bestelling.Besteldatum = DateTime.Now;
            Bestelling.Totaalbedrag = 0;
            Bestelling.BestellingProducten = new List<BestellingProduct>();

            // Producten ophalen en koppelen aan bestelling met aantallen
            var grouped = productIds
                .GroupBy(id => id)
                .Select(g => new { ProductId = g.Key, Aantal = g.Count() })
                .ToList();

            var producten = await _context.Producten
                .Where(p => grouped.Select(g => g.ProductId).Contains(p.ProductId))
                .ToListAsync();

            foreach (var groep in grouped)
            {
                var product = producten.First(p => p.ProductId == groep.ProductId);

                Bestelling.Totaalbedrag += product.Prijs * groep.Aantal;

                Bestelling.BestellingProducten.Add(new BestellingProduct
                {
                    ProductId = product.ProductId,
                    Aantal = groep.Aantal
                });
            }

            _context.Bestellingen.Add(Bestelling);
            await _context.SaveChangesAsync();

            // Winkelmand leegmaken na bestelling
            HttpContext.Session.Remove("Winkelmand");

            return RedirectToPage("./Index");
        }
    }
}
