using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class SleepRecord
{
    public int SleepId { get; set; }

    public int UserId { get; set; }

    public DateOnly SleepDate { get; set; }

    public decimal? SleepDurationHours { get; set; }

    public virtual User User { get; set; } = null!;
}
