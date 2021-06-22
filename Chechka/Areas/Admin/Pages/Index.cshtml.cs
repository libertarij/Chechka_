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
    public class IndexModel : PageModel
    {
        private readonly Chechka.DAL.Data.ApplicationDbContext _context;

        public IndexModel(Chechka.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ComputerPart> ComputerPart { get;set; }

        public async Task OnGetAsync()
        {
            ComputerPart = await _context.ComputerParts
                .Include(c => c.Group).ToListAsync();
        }
    }
}
