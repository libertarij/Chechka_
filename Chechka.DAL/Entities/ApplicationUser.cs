using Microsoft.AspNetCore.Identity;

namespace Chechka.DAL.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public byte[] AvatarImage { get; set; }
    }
}
