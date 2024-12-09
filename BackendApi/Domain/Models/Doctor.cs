using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? ContactInfo { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
