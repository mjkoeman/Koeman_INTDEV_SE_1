using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Koeman_INTDEV_SE_1.Data;
using Koeman_INTDEV_SE_1.Models;

namespace Koeman_INTDEV_SE_1.Pages.Product
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
            return Page();
        }

        [BindProperty]
        public Koeman_INTDEV_SE_1.Models.Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Producten.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
