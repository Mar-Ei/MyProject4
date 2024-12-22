using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace Domain.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool AcceptTerms { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? Verified { get; set; }
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public int? EmployeeId { get; set; }
   

    [NotMapped]
    public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }





    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ChronicCondition> ChronicConditions { get; set; } = new List<ChronicCondition>();

    public virtual ICollection<DietPlan> DietPlans { get; set; } = new List<DietPlan>();

    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    public virtual ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();

    public virtual ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();

    public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<MedicalTest> MedicalTests { get; set; } = new List<MedicalTest>();

    public virtual ICollection<PhysicalActivity> PhysicalActivities { get; set; } = new List<PhysicalActivity>();

    public virtual ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();

    public virtual ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
