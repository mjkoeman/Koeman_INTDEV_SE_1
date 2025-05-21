using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;

namespace Koeman_INTDEV_SE_1.Pages.Klant
{
    public class EditModel : PageModel
    {
        private readonly Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 _context;

        public EditModel(Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 context)
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

            var klant =  await _context.Klanten.FirstOrDefaultAsync(m => m.KlantId == id);
            if (klant == null)
            {
                return NotFound();
            }
            Klant = klant;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Klant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlantExists(Klant.KlantId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool KlantExists(int id)
        {
            return _context.Klanten.Any(e => e.KlantId == id);
        }
    }
}
