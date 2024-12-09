using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class HealthMetric
{
    public int MetricId { get; set; }

    public int UserId { get; set; }

    public DateOnly MetricDate { get; set; }

    public decimal? Weight { get; set; }

    public int? BloodPressureSystolic { get; set; }

    public int? BloodPressureDiastolic { get; set; }

    public int? HeartRate { get; set; }

    public decimal? GlucoseLevel { get; set; }

    public virtual User User { get; set; } = null!;
}
