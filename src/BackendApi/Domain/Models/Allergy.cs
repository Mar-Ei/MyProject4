using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Allergy
    {
        public int AllergyId { get; set; }
        public int? UserId { get; set; }
        public string? AllergyName { get; set; }
        public string? AllergyReaction { get; set; }

        public virtual User? User { get; set; }
    }
}
