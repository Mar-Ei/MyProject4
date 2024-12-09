using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BackendApi.Models;

public partial class MedicalContext : DbContext
{
    public MedicalContext()
    {
    }

    public MedicalContext(DbContextOptions<MedicalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<ExerciseRecord> ExerciseRecords { get; set; }

    public virtual DbSet<FamilyHistory> FamilyHistories { get; set; }

    public virtual DbSet<HealthMetric> HealthMetrics { get; set; }

    public virtual DbSet<Immunization> Immunizations { get; set; }

    public virtual DbSet<InsurancePolicy> InsurancePolicies { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<MedicalDiary> MedicalDiaries { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<MentalHealthRecord> MentalHealthRecords { get; set; }

    public virtual DbSet<NutritionRecord> NutritionRecords { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<SleepRecord> SleepRecords { get; set; }

    public virtual DbSet<Symptom> Symptoms { get; set; }

    public virtual DbSet<TreatmentPlan> TreatmentPlans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VitalSign> VitalSigns { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PK__Allergie__ACDD0692D953F158");

            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.Allergen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("allergen");
            entity.Property(e => e.Reaction)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reaction");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Allergies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Allergies__user___4316F928");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__A50828FC00CAB77A");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__docto__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__user___4AB81AF0");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__F3993564DFBE979D");

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contact_info");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("specialization");
        });

        modelBuilder.Entity<ExerciseRecord>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418E8C1A4E6F");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.ExerciseDate).HasColumnName("exercise_date");
            entity.Property(e => e.ExerciseType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("exercise_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ExerciseRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExerciseR__user___5165187F");
        });

        modelBuilder.Entity<FamilyHistory>(entity =>
        {
            entity.HasKey(e => e.FamilyHistoryId).HasName("PK__FamilyHi__5AE3CD57D6EE42A8");

            entity.Property(e => e.FamilyHistoryId).HasColumnName("family_history_id");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("condition");
            entity.Property(e => e.Relative)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("relative");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.FamilyHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FamilyHis__user___5CD6CB2B");
        });

        modelBuilder.Entity<HealthMetric>(entity =>
        {
            entity.HasKey(e => e.MetricId).HasName("PK__HealthMe__13D5DCA4B5DFDCBE");

            entity.Property(e => e.MetricId).HasColumnName("metric_id");
            entity.Property(e => e.BloodPressureDiastolic).HasColumnName("blood_pressure_diastolic");
            entity.Property(e => e.BloodPressureSystolic).HasColumnName("blood_pressure_systolic");
            entity.Property(e => e.GlucoseLevel)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("glucose_level");
            entity.Property(e => e.HeartRate).HasColumnName("heart_rate");
            entity.Property(e => e.MetricDate).HasColumnName("metric_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.HealthMetrics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthMet__user___3D5E1FD2");
        });

        modelBuilder.Entity<Immunization>(entity =>
        {
            entity.HasKey(e => e.ImmunizationId).HasName("PK__Immuniza__F4712B41C91742E0");

            entity.Property(e => e.ImmunizationId).HasColumnName("immunization_id");
            entity.Property(e => e.ImmunizationDate).HasColumnName("immunization_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VaccineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("vaccine_name");

            entity.HasOne(d => d.User).WithMany(p => p.Immunizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Immunizat__user___45F365D3");
        });

        modelBuilder.Entity<InsurancePolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Insuranc__47DA3F031FEEB3F3");

            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.CoverageDetails)
                .HasColumnType("text")
                .HasColumnName("coverage_details");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("policy_number");
            entity.Property(e => e.Provider)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("provider");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Insurance__user___59FA5E80");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__LabTests__F3FF1C02917DCDD4");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("result");
            entity.Property(e => e.TestDate).HasColumnName("test_date");
            entity.Property(e => e.TestName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("test_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTests__user_i__4E88ABD4");
        });

        modelBuilder.Entity<MedicalDiary>(entity =>
        {
            entity.HasKey(e => e.DiaryId).HasName("PK__MedicalD__339232C8126425F2");

            entity.Property(e => e.DiaryId).HasColumnName("diary_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.MedicalDiaries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalDi__user___68487DD7");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MedicalR__BFCFB4DDC7846EF4");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__user___3A81B327");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PK__Medicati__DD94789B1D1A826E");

            entity.Property(e => e.MedicationId).HasColumnName("medication_id");
            entity.Property(e => e.Dosage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dosage");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MedicationName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("medication_name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Medications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicatio__user___403A8C7D");
        });

        modelBuilder.Entity<MentalHealthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MentalHe__BFCFB4DD176FE435");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Mood)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mood");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.StressLevel).HasColumnName("stress_level");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.MentalHealthRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MentalHea__user___656C112C");
        });

        modelBuilder.Entity<NutritionRecord>(entity =>
        {
            entity.HasKey(e => e.NutritionId).HasName("PK__Nutritio__147CC3A2DC4D381E");

            entity.Property(e => e.NutritionId).HasColumnName("nutrition_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.MealType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meal_type");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.NutritionDate).HasColumnName("nutrition_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.NutritionRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__user___5441852A");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Reminder__E27A36281C7E2EA8");

            entity.Property(e => e.ReminderId).HasColumnName("reminder_id");
            entity.Property(e => e.ReminderDate)
                .HasColumnType("datetime")
                .HasColumnName("reminder_date");
            entity.Property(e => e.ReminderText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reminder_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reminders__user___6E01572D");
        });

        modelBuilder.Entity<SleepRecord>(entity =>
        {
            entity.HasKey(e => e.SleepId).HasName("PK__SleepRec__8348CEBF42D410FB");

            entity.Property(e => e.SleepId).HasColumnName("sleep_id");
            entity.Property(e => e.SleepDate).HasColumnName("sleep_date");
            entity.Property(e => e.SleepDurationHours)
                .HasColumnType("decimal(3, 1)")
                .HasColumnName("sleep_duration_hours");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SleepRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SleepReco__user___571DF1D5");
        });

        modelBuilder.Entity<Symptom>(entity =>
        {
            entity.HasKey(e => e.SymptomId).HasName("PK__Symptoms__7A85ADB81A304E66");

            entity.Property(e => e.SymptomId).HasColumnName("symptom_id");
            entity.Property(e => e.Severity).HasColumnName("severity");
            entity.Property(e => e.Symptom1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("symptom");
            entity.Property(e => e.SymptomDate).HasColumnName("symptom_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Symptoms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Symptoms__user_i__5FB337D6");
        });

        modelBuilder.Entity<TreatmentPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Treatmen__BE9F8F1DB1A07C87");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TreatmentDescription)
                .HasColumnType("text")
                .HasColumnName("treatment_description");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TreatmentPlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Treatment__user___628FA481");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FF4E3DF84");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61640632379E").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<VitalSign>(entity =>
        {
            entity.HasKey(e => e.VitalSignId).HasName("PK__VitalSig__022B0744ADF7302E");

            entity.Property(e => e.VitalSignId).HasColumnName("vital_sign_id");
            entity.Property(e => e.BodyTemperature)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("body_temperature");
            entity.Property(e => e.MeasureDate).HasColumnName("measure_date");
            entity.Property(e => e.RespiratoryRate).HasColumnName("respiratory_rate");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.VitalSigns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VitalSign__user___6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
