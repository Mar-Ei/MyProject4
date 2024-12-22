using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Symptom
{
    public int SymptomId { get; set; }

    public int? RecordId { get; set; }

    public string? SymptomDescription { get; set; }

    public int? SymptomSeverity { get; set; }

    public virtual MedicalRecord? Record { get; set; }
}
