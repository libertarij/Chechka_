using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chechka.DAL.Data;
using Chechka.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Chechka.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly Chechka.DAL.Data.ApplicationDbContext _context;

        //Lb8.4.4.4{
        private readonly IWebHostEnvironment _environment;
        //Lb8.4.4.4}

        public EditModel(Chechka.DAL.Data.ApplicationDbContext context
            //Lb8.4.4.4{
            ,
            IWebHostEnvironment env
            //Lb8.4.4.4}
            )
        {
            _context = context;
            //Lb8.4.4.4{
            _environment = env;
            //Lb8.4.4.4}
        }

        [BindProperty]
        public ComputerPart ComputerPart { get; set; }

        //Lb8.4.4.4{
        [BindProperty]
        public IFormFile Image { get; set; }
        //Lb8.4.4.4}

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

            ////Lb8.4.4.4--
            //ViewData["ComputerPartGroupId"] = new SelectList(_context.ComputerPartGroups, "ComputerPartGroupId", "ComputerPartGroupId");
            ////Lb8.4.4.4--

            //Lb8.4.4.4{
            ViewData["ComputerPartGroupId"] = new SelectList(_context.ComputerPartGroups, "ComputerPartGroupId", "GroupName");
            //Lb8.4.4.4}

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Lb8.4.4.4{
            if (Image != null)
            {
                var fileName = $"{ComputerPart.ComputerPartId}" +
                Path.GetExtension(Image.FileName);
                ComputerPart.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images",
                fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }
             //Lb8.4.4.4{

                _context.Attach(ComputerPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerPartExists(ComputerPart.ComputerPartId))
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

        private bool ComputerPartExists(int id)
        {
            return _context.ComputerParts.Any(e => e.ComputerPartId == id);
        }
    }
}
