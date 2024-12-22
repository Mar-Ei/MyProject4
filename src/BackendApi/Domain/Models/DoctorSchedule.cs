using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class DoctorSchedule
    {
        public int ScheduleId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}
