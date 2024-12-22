using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class LabResult
{
    public int ResultId { get; set; }

    public int? UserId { get; set; }

    public string? TestName { get; set; }

    public string? ResultValue { get; set; }

    public DateOnly? ResultDate { get; set; }

    public virtual User? User { get; set; }
}
