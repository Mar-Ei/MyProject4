using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Allergy
{
    public int AllergyId { get; set; }

    public int UserId { get; set; }

    public string Allergen { get; set; } = null!;

    public string? Reaction { get; set; }

    public virtual User User { get; set; } = null!;
}
