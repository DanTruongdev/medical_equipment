using Castle.Core.Internal;
using MedicalEquipmentWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalEquipmentWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
           
            var user = await _userManager.GetUserAsync(User);
            string name = user == null ? "Guest" : user.ToString();
            return View("Index", name);
        }
    }
}
