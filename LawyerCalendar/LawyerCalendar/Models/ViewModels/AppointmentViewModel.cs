using LawyerCalendar.BusinessLogic;
using System;
using System.ComponentModel.DataAnnotations;

namespace LawyerCalendar.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }        
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Fecha")]
        [AppointmentValidations]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Especialidad")]
        public int SpecialtyId { get; set; }
    }
}
