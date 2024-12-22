using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class DoctorSchedule
{
    public int ScheduleId { get; set; }

    public int? DoctorId { get; set; }

    public DateOnly? ScheduleDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
