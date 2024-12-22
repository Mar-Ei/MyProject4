using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public TimeOnly? AppointmentTime { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<AppointmentsFeedback> AppointmentsFeedbacks { get; set; } = new List<AppointmentsFeedback>();

    public virtual Doctor? Doctor { get; set; }

    public virtual User? User { get; set; }
}
