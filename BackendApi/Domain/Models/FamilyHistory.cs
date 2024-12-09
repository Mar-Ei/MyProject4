using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class FamilyHistory
{
    public int FamilyHistoryId { get; set; }

    public int UserId { get; set; }

    public string? Relative { get; set; }

    public string? Condition { get; set; }

    public virtual User User { get; set; } = null!;
}
