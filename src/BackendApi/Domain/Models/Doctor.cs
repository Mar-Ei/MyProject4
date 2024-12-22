using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            DoctorSchedules = new HashSet<DoctorSchedule>();
            MedicalRecords = new HashSet<MedicalRecord>();
        }

        public int DoctorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Specialty { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
