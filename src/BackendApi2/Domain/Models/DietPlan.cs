using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class DietPlan
{
    public int DietPlanId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? PlanDate { get; set; }

    public string? Description { get; set; }

    public virtual User? User { get; set; }
}
