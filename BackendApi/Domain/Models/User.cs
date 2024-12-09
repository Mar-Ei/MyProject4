using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ExerciseRecord> ExerciseRecords { get; set; } = new List<ExerciseRecord>();

    public virtual ICollection<FamilyHistory> FamilyHistories { get; set; } = new List<FamilyHistory>();

    public virtual ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();

    public virtual ICollection<Immunization> Immunizations { get; set; } = new List<Immunization>();

    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();

    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    public virtual ICollection<MedicalDiary> MedicalDiaries { get; set; } = new List<MedicalDiary>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

    public virtual ICollection<MentalHealthRecord> MentalHealthRecords { get; set; } = new List<MentalHealthRecord>();

    public virtual ICollection<NutritionRecord> NutritionRecords { get; set; } = new List<NutritionRecord>();

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<SleepRecord> SleepRecords { get; set; } = new List<SleepRecord>();

    public virtual ICollection<Symptom> Symptoms { get; set; } = new List<Symptom>();

    public virtual ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();

    public virtual ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();
}
