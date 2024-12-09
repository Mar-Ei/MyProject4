using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class VitalSign
{
    public int VitalSignId { get; set; }

    public int UserId { get; set; }

    public DateOnly MeasureDate { get; set; }

    public decimal? BodyTemperature { get; set; }

    public int? RespiratoryRate { get; set; }

    public virtual User User { get; set; } = null!;
}
