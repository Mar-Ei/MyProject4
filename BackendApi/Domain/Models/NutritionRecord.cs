using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class NutritionRecord
{
    public int NutritionId { get; set; }

    public int UserId { get; set; }

    public DateOnly NutritionDate { get; set; }

    public string? MealType { get; set; }

    public int? Calories { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
