using System;
using System.Collections.Generic;

#nullable disable

namespace LawyerCalendar.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Date { get; set; }
        public int? SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual User User { get; set; }
    }
}
