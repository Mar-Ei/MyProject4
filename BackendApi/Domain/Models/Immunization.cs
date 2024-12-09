using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Immunization
{
    public int ImmunizationId { get; set; }

    public int UserId { get; set; }

    public string VaccineName { get; set; } = null!;

    public DateOnly? ImmunizationDate { get; set; }

    public virtual User User { get; set; } = null!;
}
