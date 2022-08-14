using LawyerCalendar.Helpers;
using LawyerCalendar.Models;
using LawyerCalendar.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LawyerCalendar.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private LawyerCalendarContext _context;

        public AppointmentController(LawyerCalendarContext context) => _context = context;

        public IActionResult Index()
        {            
            List<Appointment> appointmentsEF = _context
                .Appointments
                .Include(e => e.Specialty)
                .Where(e => e.UserId == _GetUserId())
                .ToList()
                .OrderByDescending(e => Convert.ToDateTime(e.Date).Date)
                .ToList();
            List<AppointmentViewModel> appointments = AppointmentHelper.ViewModelBuilder(appointmentsEF);
            return View(appointments);
        }

        public IActionResult DeleteAppointment(int id)
        {
            Appointment appointmentToDelete = _context.Appointments.FirstOrDefault(e => e.Id == id);

            if(appointmentToDelete != null)
            {
                _context.Appointments.Remove(appointmentToDelete);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
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
                return Content("<script language='javascript' type='text/javascript'>alert('No hay espacio disponible para la fecha y hora especificado');</script>");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Specialties"] = new SelectList(_context.Specialties, "Id", "Name", appointment.SpecialtyId);
                return View();
            }

            Appointment appointmentEF = new Appointment()
            {
                UserId = _GetUserId(),
                Date = appointment.Date.ToShortDateString(),
                SpecialtyId = appointment.SpecialtyId
            };

            _context.Add(appointmentEF);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private int _GetUserId() => Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
    }
}
