using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class MedicalContext : DbContext
    {
   
            public DbSet<User> Users { get; set; }
            public DbSet<Vaccination> Vaccinations { get; set; }
            public DbSet<Allergy> Allergies { get; set; }
            public DbSet<Appointment> Appointments { get; set; }
            public DbSet<AppointmentsFeedback> AppointmentsFeedbacks { get; set; }
            public DbSet<ChronicCondition> ChronicConditions { get; set; }
            public DbSet<DietPlan> DietPlans { get; set; }
            public DbSet<Doctor> Doctors { get; set; }
            public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
            public DbSet<EmergencyContact> EmergencyContacts { get; set; }
            public DbSet<HealthMetric> HealthMetrics { get; set; }
            public DbSet<LabResult> LabResults { get; set; }
            public DbSet<MedicalHistory> MedicalHistories { get; set; }
            public DbSet<MedicalRecord> MedicalRecords { get; set; }
            public DbSet<MedicalTest> MedicalTests { get; set; }
            public DbSet<Medication> Medications { get; set; }
            public DbSet<PhysicalActivity> PhysicalActivities { get; set; }
            public DbSet<Symptom> Symptoms { get; set; }
            public DbSet<UserActivity> UserActivities { get; set; }
            public DbSet<UserMessage> UserMessages { get; set; }

            public MedicalContext()
            {
            }

            public MedicalContext(DbContextOptions<MedicalContext> options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Настройки для сущности Vaccination
                modelBuilder.Entity<Vaccination>(entity =>
                {
                    entity.Property(e => e.VaccineName)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("vaccine_name");

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.Vaccinations)
                        .HasForeignKey(d => d.UserId)
                        .HasConstraintName("FK__Vaccinati__user___5DCAEF64");
                });

                // Настройки для сущности Allergy
                modelBuilder.Entity<Allergy>(entity =>
                {
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

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.Allergies)
                        .HasForeignKey(d => d.UserId)
                        .HasConstraintName("FK__Allergies__user___5DCAEF64");
                });

                modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("date")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.AppointmentTime).HasColumnName("appointment_time");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Appointme__docto__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Appointme__user___4316F928");
            });

            modelBuilder.Entity<AppointmentsFeedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackId)
                    .HasName("PK__Appointm__7A6B2B8C12DA18A4");

                entity.ToTable("AppointmentsFeedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.FeedbackText)
                    .HasColumnType("text")
                    .HasColumnName("feedback_text");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentsFeedbacks)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__Appointme__appoi__4F7CD00D");
            });

            modelBuilder.Entity<ChronicCondition>(entity =>
            {
                entity.HasKey(e => e.ConditionId)
                    .HasName("PK__ChronicC__8527AB15C93F3AC8");

                entity.Property(e => e.ConditionId).HasColumnName("condition_id");

                entity.Property(e => e.ConditionName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("condition_name");

                entity.Property(e => e.DiagnosisDate)
                    .HasColumnType("date")
                    .HasColumnName("diagnosis_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChronicConditions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ChronicCo__user___6383C8BA");
            });

            modelBuilder.Entity<DietPlan>(entity =>
            {
                entity.Property(e => e.DietPlanId).HasColumnName("diet_plan_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.PlanDate)
                    .HasColumnType("date")
                    .HasColumnName("plan_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DietPlans)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__DietPlans__user___52593CB8");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
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
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK__DoctorSc__C46A8A6FDE0828F2");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.ScheduleDate)
                    .HasColumnType("date")
                    .HasColumnName("schedule_date");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorSchedules)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__DoctorSch__docto__6C190EBB");
            });

            modelBuilder.Entity<EmergencyContact>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__Emergenc__024E7A862C78399C");

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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmergencyContacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Emergency__user___60A75C0F");
            });

            modelBuilder.Entity<HealthMetric>(entity =>
            {
                entity.HasKey(e => e.MetricId)
                    .HasName("PK__HealthMe__13D5DCA42F934BEA");

                entity.Property(e => e.MetricId).HasColumnName("metric_id");

                entity.Property(e => e.BloodPressure)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("blood_pressure");

                entity.Property(e => e.HeartRate).HasColumnName("heart_rate");

                entity.Property(e => e.Height)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("height");

                entity.Property(e => e.MeasurementDate)
                    .HasColumnType("date")
                    .HasColumnName("measurement_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("weight");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HealthMetrics)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__HealthMet__user___403A8C7D");
            });

            modelBuilder.Entity<LabResult>(entity =>
            {
                entity.HasKey(e => e.ResultId)
                    .HasName("PK__LabResul__AFB3C3160D0DC6F5");

                entity.Property(e => e.ResultId).HasColumnName("result_id");

                entity.Property(e => e.ResultDate)
                    .HasColumnType("date")
                    .HasColumnName("result_date");

                entity.Property(e => e.ResultValue)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("result_value");

                entity.Property(e => e.TestName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("test_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LabResults)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__LabResult__user___66603565");
            });

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__MedicalH__096AA2E963C61A51");

                entity.ToTable("MedicalHistory");

                entity.Property(e => e.HistoryId).HasColumnName("history_id");

                entity.Property(e => e.Condition)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("condition");

                entity.Property(e => e.HistoryDate)
                    .HasColumnType("date")
                    .HasColumnName("history_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MedicalHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MedicalHi__user___49C3F6B7");
            });

            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__MedicalR__BFCFB4DD666B3815");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.Diagnosis)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("diagnosis");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("date")
                    .HasColumnName("record_date");

                entity.Property(e => e.Treatment)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("treatment");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__MedicalRe__docto__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MedicalRe__user___3C69FB99");
            });

            modelBuilder.Entity<MedicalTest>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PK__MedicalT__F3FF1C0237F36F51");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.Result)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("result");

                entity.Property(e => e.TestDate)
                    .HasColumnType("date")
                    .HasColumnName("test_date");

                entity.Property(e => e.TestName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("test_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MedicalTests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MedicalTe__user___5812160E");
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.Property(e => e.MedicationId).HasColumnName("medication_id");

                entity.Property(e => e.Dosage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dosage");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.MedicationName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("medication_name");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.Medications)
                    .HasForeignKey(d => d.RecordId)
                    .HasConstraintName("FK__Medicatio__recor__46E78A0C");
            });

            modelBuilder.Entity<PhysicalActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__Physical__482FBD63DC0F8761");

                entity.ToTable("PhysicalActivity");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("activity_date");

                entity.Property(e => e.ActivityType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("activity_type");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhysicalActivities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PhysicalA__user___5535A963");
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.Property(e => e.SymptomId).HasColumnName("symptom_id");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.SymptomDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("symptom_description");

                entity.Property(e => e.SymptomSeverity).HasColumnName("symptom_severity");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.Symptoms)
                    .HasForeignKey(d => d.RecordId)
                    .HasConstraintName("FK__Symptoms__record__4CA06362");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__AB6E6164B7FD7CA6")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.BirthDate)  // Исправленное название свойства
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)  // Исправленное название свойства
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.Lastname)  // Исправленное название свойства
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)  // Используйте Password вместо PasswordHash
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password_hash");
            });


            modelBuilder.Entity<UserActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__UserActi__482FBD6329146C3E");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("activity_date");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("activity_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserActivities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserActiv__user___6EF57B66");
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__UserMess__0BBF6EE664CDD6F9");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.MessageDate)
                    .HasColumnType("date")
                    .HasColumnName("message_date");

                entity.Property(e => e.MessageText)
                    .HasColumnType("text")
                    .HasColumnName("message_text");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMessages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserMessa__user___693CA210");
            });

            modelBuilder.Entity<Vaccination>(entity =>
            {
                entity.Property(e => e.VaccinationId).HasColumnName("vaccination_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VaccinationDate)
                    .HasColumnType("date")
                    .HasColumnName("vaccination_date");

                entity.Property(e => e.VaccineName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("vaccine_name");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vaccinations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Vaccinati__user___5DCAEF64");
            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
