using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class TreatmentPlan
{
    public int PlanId { get; set; }

    public int UserId { get; set; }

    public string TreatmentDescription { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual User User { get; set; } = null!;
}
