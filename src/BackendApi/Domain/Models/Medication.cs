using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Medication
    {
        public int MedicationId { get; set; }
        public int? RecordId { get; set; }
        public string? MedicationName { get; set; }
        public string? Dosage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual MedicalRecord? Record { get; set; }
    }
}
