using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserActivity
{
    public int ActivityId { get; set; }

    public int? UserId { get; set; }

    public string? ActivityName { get; set; }

    public DateOnly? ActivityDate { get; set; }

    public virtual User? User { get; set; }
}
