using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class LabTest
{
    public int TestId { get; set; }

    public int UserId { get; set; }

    public string TestName { get; set; } = null!;

    public DateOnly TestDate { get; set; }

    public string? Result { get; set; }

    public virtual User User { get; set; } = null!;
}
