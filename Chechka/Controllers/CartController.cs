using Chechka.DAL.Data;
using Chechka.Extensions;
using Chechka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Chechka.Controllers
{

   //Lb8.4.5.4{
   public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private string cartKey = "cart";
        private Cart _cart;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>(cartKey);

            return View(_cart.Items.Values);
        }

        [Authorize]
        public IActionResult Add(int id, string returnUrl)

        {

            _cart = HttpContext.Session.Get<Cart>(cartKey);

            var item = _context.ComputerParts.Find(id);
            if (item != null)
            {
                _cart.AddToCart(item);
                HttpContext.Session.Set<Cart>(cartKey, _cart);
            }
            return Redirect(returnUrl);
        }
    }
    //Lb8.4.5.4
}
