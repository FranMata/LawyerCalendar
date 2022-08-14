using LawyerCalendar.Models;
using LawyerCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LawyerCalendar.Controllers
{
    public class LoginController : Controller
    {
        private readonly LawyerCalendarContext _context;
        public LoginController(LawyerCalendarContext context) => _context = context;
        public IActionResult Index() => View();
        
        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel user)
        {
            User userEF = _context.Users.FirstOrDefault(e => e.BirthDate.Equals(user.BirthDate.ToShortDateString()) && e.IdDocument == user.IdDocument);

            if (userEF == null)
                return View();

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userEF.Name),
                new Claim("Id", userEF.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Appointment");
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}
