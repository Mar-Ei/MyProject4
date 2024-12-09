using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class ExerciseRecord
{
    public int ExerciseId { get; set; }

    public int UserId { get; set; }

    public DateOnly ExerciseDate { get; set; }

    public string? ExerciseType { get; set; }

    public int? DurationMinutes { get; set; }

    public virtual User User { get; set; } = null!;
}
