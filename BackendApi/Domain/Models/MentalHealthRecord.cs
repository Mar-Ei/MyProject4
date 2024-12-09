using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class MentalHealthRecord
{
    public int RecordId { get; set; }

    public int UserId { get; set; }

    public DateOnly RecordDate { get; set; }

    public string? Mood { get; set; }

    public int? StressLevel { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
