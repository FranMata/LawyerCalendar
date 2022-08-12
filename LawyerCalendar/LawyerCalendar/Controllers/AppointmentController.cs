using LawyerCalendar.Models;
using LawyerCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace LawyerCalendar.Controllers
{
    public class AppointmentController : Controller
    {
        private LawyerCalendarContext _context;

        public AppointmentController(LawyerCalendarContext context) => _context = context;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateAppointment()
        {
            ViewData["Specialties"] = new SelectList(_context.Specialties, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateAppointment(AppointmentViewModel appointment)
        {
            bool isspaceAvailable = _context.Appointments.FirstOrDefault(e => e.SpecialtyId == appointment.SpecialtyId && e.Date.Equals(appointment.Date.ToString())) != null;

            if (isspaceAvailable)
            {
                //TODO: mostrar mensaje
                return View();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Specialties"] = new SelectList(_context.Specialties, "Id", "Name", appointment.SpecialtyId);
                return View();
            }

            Appointment appointmentEF = new Appointment()
            {
                //UserId = TODO: obtener el id del usu4rio de la cookie
                Date = appointment.Date.ToShortDateString(),
                SpecialtyId = appointment.SpecialtyId
            };

            _context.Add(appointmentEF);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
