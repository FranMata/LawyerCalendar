using System;
using System.ComponentModel.DataAnnotations;

namespace LawyerCalendar.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cedula")]
        public int IdDocument { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Metodo de pago")]
        public int PaymentMethodId { get; set; }

        [Required]
        [Display(Name = "Informacion del metodo de pago")]
        public string PaymentMethodData { get; set; }
    }
}