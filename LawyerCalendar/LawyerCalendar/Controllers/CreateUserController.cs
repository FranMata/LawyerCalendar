using LawyerCalendar.Models;
using LawyerCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerCalendar.Controllers
{
    public class CreateUserController : Controller
    {
        private readonly LawyerCalendarContext _context;

        public CreateUserController(LawyerCalendarContext context) => _context = context;

        public IActionResult Index()
        {
            ViewData["PaymentMethods"] = new SelectList(_context.PaymentMethods, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (!ModelState.IsValid || !_ValidateAge(user.BirthDate))
                return View();

            User userEF = new User()
            {
                IdDocument = user.IdDocument,
                BirthDate = user.BirthDate.ToShortDateString(),
                Name = user.Name,
                PaymentMethodId = user.PaymentMethodId,
                PaymentMethodData = user.PaymentMethodData
            };

            _context.Add(userEF);
            await _context.SaveChangesAsync();
            return Redirect(@"/");
        }

        private bool _ValidateAge(DateTime birthDate) => DateTime.Now.AddYears(-15) < birthDate;
    }
}
