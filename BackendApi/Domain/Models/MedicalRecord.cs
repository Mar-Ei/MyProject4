using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int UserId { get; set; }

    public DateOnly RecordDate { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
