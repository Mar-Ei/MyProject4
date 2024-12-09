using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Symptom
{
    public int SymptomId { get; set; }

    public int UserId { get; set; }

    public DateOnly SymptomDate { get; set; }

    public string Symptom1 { get; set; } = null!;

    public int? Severity { get; set; }

    public virtual User User { get; set; } = null!;
}
