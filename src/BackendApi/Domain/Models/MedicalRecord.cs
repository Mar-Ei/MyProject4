using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class MedicalRecord
    {
        public MedicalRecord()
        {
            Medications = new HashSet<Medication>();
            Symptoms = new HashSet<Symptom>();
        }

        public int RecordId { get; set; }
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? RecordDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
        public virtual ICollection<Symptom> Symptoms { get; set; }
    }
}
