using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Medication
{
    public int MedicationId { get; set; }

    public int? RecordId { get; set; }

    public string? MedicationName { get; set; }

    public string? Dosage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual MedicalRecord? Record { get; set; }
}
