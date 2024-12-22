using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

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

    public virtual DbSet<AppointmentsFeedback> AppointmentsFeedbacks { get; set; }

    public virtual DbSet<ChronicCondition> ChronicConditions { get; set; }

    public virtual DbSet<DietPlan> DietPlans { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<HealthMetric> HealthMetrics { get; set; }

    public virtual DbSet<LabResult> LabResults { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<MedicalTest> MedicalTests { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<PhysicalActivity> PhysicalActivities { get; set; }

    public virtual DbSet<Symptom> Symptoms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserActivity> UserActivities { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PK__Allergie__ACDD0692F85E1499");

            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.AllergyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("allergy_name");
            entity.Property(e => e.AllergyReaction)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("allergy_reaction");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Allergies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Allergies__user___162F4418");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__A50828FCF1C59315");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointment_date");
            entity.Property(e => e.AppointmentTime).HasColumnName("appointment_time");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Appointme__docto__7F4BDEC0");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Appointme__user___7E57BA87");
        });

        modelBuilder.Entity<AppointmentsFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Appointm__7A6B2B8CBF2C611D");

            entity.ToTable("AppointmentsFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.FeedbackText)
                .HasColumnType("text")
                .HasColumnName("feedback_text");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentsFeedbacks)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__appoi__0ABD916C");
        });

        modelBuilder.Entity<ChronicCondition>(entity =>
        {
            entity.HasKey(e => e.ConditionId).HasName("PK__ChronicC__8527AB15681E9989");

            entity.Property(e => e.ConditionId).HasColumnName("condition_id");
            entity.Property(e => e.ConditionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("condition_name");
            entity.Property(e => e.DiagnosisDate).HasColumnName("diagnosis_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ChronicConditions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ChronicCo__user___1EC48A19");
        });

        modelBuilder.Entity<DietPlan>(entity =>
        {
            entity.HasKey(e => e.DietPlanId).HasName("PK__DietPlan__C9868B065831F9AD");

            entity.Property(e => e.DietPlanId).HasColumnName("diet_plan_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.PlanDate).HasColumnName("plan_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.DietPlans)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DietPlans__user___0D99FE17");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__F399356413229E43");

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("specialty");
        });

        modelBuilder.Entity<DoctorSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__DoctorSc__C46A8A6F77B45F2E");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.ScheduleDate).HasColumnName("schedule_date");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__DoctorSch__docto__2759D01A");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Emergenc__024E7A8651A787EB");

            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.ContactName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contact_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relationship");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Emergency__user___1BE81D6E");
        });

        modelBuilder.Entity<HealthMetric>(entity =>
        {
            entity.HasKey(e => e.MetricId).HasName("PK__HealthMe__13D5DCA46A1FF1D9");

            entity.Property(e => e.MetricId).HasColumnName("metric_id");
            entity.Property(e => e.BloodPressure)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("blood_pressure");
            entity.Property(e => e.HeartRate).HasColumnName("heart_rate");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height");
            entity.Property(e => e.MeasurementDate).HasColumnName("measurement_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.HealthMetrics)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HealthMet__user___7B7B4DDC");
        });

        modelBuilder.Entity<LabResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__LabResul__AFB3C31609759600");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.ResultDate).HasColumnName("result_date");
            entity.Property(e => e.ResultValue)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("result_value");
            entity.Property(e => e.TestName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("test_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.LabResults)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__LabResult__user___21A0F6C4");
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__MedicalH__096AA2E9E89394B3");

            entity.ToTable("MedicalHistory");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("condition");
            entity.Property(e => e.HistoryDate).HasColumnName("history_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.MedicalHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MedicalHi__user___0504B816");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MedicalR__BFCFB4DD1EFADDD3");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.Treatment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("treatment");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__MedicalRe__docto__789EE131");

            entity.HasOne(d => d.User).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MedicalRe__user___77AABCF8");
        });

        modelBuilder.Entity<MedicalTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__MedicalT__F3FF1C02D4E2CBE7");

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

            entity.HasOne(d => d.User).WithMany(p => p.MedicalTests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MedicalTe__user___1352D76D");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PK__Medicati__DD94789B178E9887");

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
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Record).WithMany(p => p.Medications)
                .HasForeignKey(d => d.RecordId)
                .HasConstraintName("FK__Medicatio__recor__02284B6B");
        });

        modelBuilder.Entity<PhysicalActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__Physical__482FBD6300775B52");

            entity.ToTable("PhysicalActivity");

            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.ActivityDate).HasColumnName("activity_date");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("activity_type");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PhysicalActivities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PhysicalA__user___10766AC2");
        });

        modelBuilder.Entity<Symptom>(entity =>
        {
            entity.HasKey(e => e.SymptomId).HasName("PK__Symptoms__7A85ADB8B3CED3CE");

            entity.Property(e => e.SymptomId).HasColumnName("symptom_id");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.SymptomDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("symptom_description");
            entity.Property(e => e.SymptomSeverity).HasColumnName("symptom_severity");

            entity.HasOne(d => d.Record).WithMany(p => p.Symptoms)
                .HasForeignKey(d => d.RecordId)
                .HasConstraintName("FK__Symptoms__record__07E124C1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370F121A8E75");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164AD19EBC5").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
        });

        modelBuilder.Entity<UserActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__UserActi__482FBD6374FE37BA");

            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.ActivityDate).HasColumnName("activity_date");
            entity.Property(e => e.ActivityName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("activity_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserActivities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserActiv__user___2A363CC5");
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__UserMess__0BBF6EE6ACE73732");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.MessageDate).HasColumnName("message_date");
            entity.Property(e => e.MessageText)
                .HasColumnType("text")
                .HasColumnName("message_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserMessages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserMessa__user___247D636F");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.VaccinationId).HasName("PK__Vaccinat__E588AFE71B7FBAA0");

            entity.Property(e => e.VaccinationId).HasColumnName("vaccination_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VaccinationDate).HasColumnName("vaccination_date");
            entity.Property(e => e.VaccineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("vaccine_name");

            entity.HasOne(d => d.User).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Vaccinati__user___190BB0C3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
