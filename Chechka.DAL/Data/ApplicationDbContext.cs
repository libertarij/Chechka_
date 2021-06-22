using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Chechka.DAL.Entities;

namespace Chechka.DAL.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Lb8.4.1{
        public DbSet<ComputerPart> ComputerParts { get; set; }
        public DbSet<ComputerPartGroup> ComputerPartGroups { get; set; }
        //Lb8.4.1}
    }
}
