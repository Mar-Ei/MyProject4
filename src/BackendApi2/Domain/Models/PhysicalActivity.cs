using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class PhysicalActivity
{
    public int ActivityId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? ActivityDate { get; set; }

    public string? ActivityType { get; set; }

    public int? Duration { get; set; }

    public virtual User? User { get; set; }
}
