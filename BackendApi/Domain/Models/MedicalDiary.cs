using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class MedicalDiary
{
    public int DiaryId { get; set; }

    public int UserId { get; set; }

    public DateOnly EntryDate { get; set; }

    public string? Content { get; set; }

    public virtual User User { get; set; } = null!;
}
