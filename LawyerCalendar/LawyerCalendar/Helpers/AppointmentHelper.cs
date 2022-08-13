using LawyerCalendar.Models;
using LawyerCalendar.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace LawyerCalendar.Helpers
{
    public static class AppointmentHelper
    {
        public static List<AppointmentViewModel> ViewModelBuilder(List<Appointment> appointments)
        {
            List<AppointmentViewModel> appointmentViewModels = new List<AppointmentViewModel>();

            appointments.ForEach(e =>
            {
                string state = Convert.ToDateTime(e.Date) < DateTime.Now ? "Acudio" : "Activo";
                appointmentViewModels.Add(
                    new AppointmentViewModel()
                    {
                        Id = e.Id,
                        UserId = (int)e.UserId,
                        Date = Convert.ToDateTime(e.Date),
                        SpecialtyId = (int)e.SpecialtyId,
                        SpecialtyName = e.Specialty.Name,
                        State = state
                    });
            });
            return appointmentViewModels;
        }
    }
}
