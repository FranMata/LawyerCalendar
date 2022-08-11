using System;
using System.Collections.Generic;

#nullable disable

namespace LawyerCalendar.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int? IdDocument { get; set; }
        public string BirthDate { get; set; }
        public string Name { get; set; }
        public int? PaymentMethodId { get; set; }
        public string PaymentMethodData { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
