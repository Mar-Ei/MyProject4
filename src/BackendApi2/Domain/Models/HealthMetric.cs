using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class HealthMetric
{
    public int MetricId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? MeasurementDate { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string? BloodPressure { get; set; }

    public int? HeartRate { get; set; }

    public virtual User? User { get; set; }
}
