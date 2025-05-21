using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;

namespace Koeman_INTDEV_SE_1.Pages.Klant
{
    public class DeleteModel : PageModel
    {
        private readonly Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 _context;

        public DeleteModel(Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 context)
        {
            _context = context;
        }

        [BindProperty]
        public Koeman_INTDEV_SE_1.Models.Klant Klant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten.FirstOrDefaultAsync(m => m.KlantId == id);

            if (klant is not null)
            {
                Klant = klant;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten.FindAsync(id);
            if (klant != null)
            {
                Klant = klant;
                _context.Klanten.Remove(Klant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
