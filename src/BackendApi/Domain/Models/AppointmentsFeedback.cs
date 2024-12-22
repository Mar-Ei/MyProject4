using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class AppointmentsFeedback
    {
        public int FeedbackId { get; set; }
        public int? AppointmentId { get; set; }
        public string? FeedbackText { get; set; }
        public int? Rating { get; set; }

        public virtual Appointment? Appointment { get; set; }
    }
}
