using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public DateOnly? RecordDate { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

    public virtual ICollection<Symptom> Symptoms { get; set; } = new List<Symptom>();

    public virtual User? User { get; set; }
}
