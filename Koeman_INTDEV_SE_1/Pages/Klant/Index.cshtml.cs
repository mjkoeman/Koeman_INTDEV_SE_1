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
    public class IndexModel : PageModel
    {
        private readonly Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 _context;

        public IndexModel(Koeman_INTDEV_SE_1.Data.DbContextINTDEV1 context)
        {
            _context = context;
        }

        public IList<Koeman_INTDEV_SE_1.Models.Klant> Klant { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Klant = await _context.Klanten.ToListAsync();
        }
    }
}
