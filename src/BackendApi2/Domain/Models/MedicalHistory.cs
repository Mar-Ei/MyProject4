using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class MedicalHistory
{
    public int HistoryId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? HistoryDate { get; set; }

    public string? Condition { get; set; }

    public virtual User? User { get; set; }
}
