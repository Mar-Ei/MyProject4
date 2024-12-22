using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Vaccination
    {
        public int VaccinationId { get; set; }
        public int? UserId { get; set; }
        public string? VaccineName { get; set; }
        public DateTime? VaccinationDate { get; set; }

        public virtual User? User { get; set; }
    }
}
