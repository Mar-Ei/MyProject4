using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Allergies = new HashSet<Allergy>();
            Appointments = new HashSet<Appointment>();
            ChronicConditions = new HashSet<ChronicCondition>();
            DietPlans = new HashSet<DietPlan>();
            EmergencyContacts = new HashSet<EmergencyContact>();
            HealthMetrics = new HashSet<HealthMetric>();
            LabResults = new HashSet<LabResult>();
            MedicalHistories = new HashSet<MedicalHistory>();
            MedicalRecords = new HashSet<MedicalRecord>();
            MedicalTests = new HashSet<MedicalTest>();
            PhysicalActivities = new HashSet<PhysicalActivity>();
            UserActivities = new HashSet<UserActivity>();
            UserMessages = new HashSet<UserMessage>();
            Vaccinations = new HashSet<Vaccination>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public virtual ICollection<Allergy> Allergies { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<ChronicCondition> ChronicConditions { get; set; }
        public virtual ICollection<DietPlan> DietPlans { get; set; }
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public virtual ICollection<HealthMetric> HealthMetrics { get; set; }
        public virtual ICollection<LabResult> LabResults { get; set; }
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<MedicalTest> MedicalTests { get; set; }
        public virtual ICollection<PhysicalActivity> PhysicalActivities { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<UserMessage> UserMessages { get; set; }
        public virtual ICollection<Vaccination> Vaccinations { get; set; }
    }
}
