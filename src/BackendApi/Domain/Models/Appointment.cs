using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            AppointmentsFeedbacks = new HashSet<AppointmentsFeedback>();
        }

        public int AppointmentId { get; set; }
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string? Status { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<AppointmentsFeedback> AppointmentsFeedbacks { get; set; }
    }
}
