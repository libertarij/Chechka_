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
    public class DeleteModel : PageModel
    {
        private readonly Chechka.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(Chechka.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComputerPart = await _context.ComputerParts.FindAsync(id);

            if (ComputerPart != null)
            {
                _context.ComputerParts.Remove(ComputerPart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
