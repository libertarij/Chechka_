using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Chechka.DAL.Data;
using Chechka.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Chechka.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Chechka.DAL.Data.ApplicationDbContext _context;

        //Lb8.4.4.4{
        private readonly IWebHostEnvironment _environment;
        //Lb8.4.4.4}

        public CreateModel(Chechka.DAL.Data.ApplicationDbContext context
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

        public IActionResult OnGet()
        {
        ViewData["ComputerPartGroupId"] = new SelectList(_context.ComputerPartGroups, "ComputerPartGroupId", "ComputerPartGroupId");
            return Page();
        }

        [BindProperty]
        public ComputerPart ComputerPart { get; set; }

        //Lb8.4.4.4{
        [BindProperty]
        public IFormFile Image { get; set; }
        //Lb8.4.4.4}

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ComputerParts.Add(ComputerPart);
            await _context.SaveChangesAsync();

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
                await _context.SaveChangesAsync();
            }
            //Lb8.4.4.4}

            return RedirectToPage("./Index");
        }
    }
}
