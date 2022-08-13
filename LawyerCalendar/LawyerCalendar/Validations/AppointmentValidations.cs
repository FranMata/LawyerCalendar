using System;
using System.ComponentModel.DataAnnotations;

namespace LawyerCalendar.BusinessLogic
{
    public class AppointmentValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime appointmentDate = Convert.ToDateTime(value);

            if (_IsWeekend(appointmentDate))
                return new ValidationResult("El horario de atencion es de lunes a viernes");
            if(!_IsOnAttentionHours(appointmentDate))
                return new ValidationResult("El horario de atencion es de 8am a 11am y de 1pm a 4pm");
            if(_IsInvalidMinutes(appointmentDate))
                return new ValidationResult("Todas las citas se dan a la hora en punto"); 
            if(!_IsOnRange(appointmentDate))
                return new ValidationResult("Solo puede gestionar citas desde el día siguiente y a más tardar 22 días después");
            return ValidationResult.Success;
        }

        private bool _IsWeekend(DateTime date) => (date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday);

        private bool _IsOnAttentionHours(DateTime date)
        {
            int hour = date.Hour;
            return (hour >= 8 && hour <= 11) || (hour >= 13 && hour <= 16);
        }

        private bool _IsInvalidMinutes(DateTime date) => date.Minute != 00;

        private bool _IsOnRange(DateTime date)
        {
            DateTime minimumDate = DateTime.Now.AddDays(1);
            DateTime maximumDate = DateTime.Now.AddDays(22);
            return date >= minimumDate && date <= maximumDate;
        }
    }
}
