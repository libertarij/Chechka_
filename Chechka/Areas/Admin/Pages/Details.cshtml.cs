using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chechka.DAL.Data;
using Chechka.DAL.Entities;

namespace Chechka.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Chechka.DAL.Data.ApplicationDbContext _context;

        public DetailsModel(Chechka.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ComputerPart ComputerPart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComputerPart = await _context.ComputerParts
                .Include(c => c.Group).FirstOrDefaultAsync(m => m.ComputerPartId == id);

            if (ComputerPart == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
