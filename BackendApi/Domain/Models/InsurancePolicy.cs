using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class InsurancePolicy
{
    public int PolicyId { get; set; }

    public int UserId { get; set; }

    public string? PolicyNumber { get; set; }

    public string? Provider { get; set; }

    public string? CoverageDetails { get; set; }

    public virtual User User { get; set; } = null!;
}
