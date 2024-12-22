using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class MedicalTest
    {
        public int TestId { get; set; }
        public int? UserId { get; set; }
        public string? TestName { get; set; }
        public DateTime? TestDate { get; set; }
        public string? Result { get; set; }

        public virtual User? User { get; set; }
    }
}
