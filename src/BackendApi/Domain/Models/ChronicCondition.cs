using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class ChronicCondition
    {
        public int ConditionId { get; set; }
        public int? UserId { get; set; }
        public string? ConditionName { get; set; }
        public DateTime? DiagnosisDate { get; set; }

        public virtual User? User { get; set; }
    }
}
