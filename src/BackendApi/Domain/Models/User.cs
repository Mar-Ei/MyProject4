using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Domain.Models
{
    public partial class User
    {
        // Новый формат для UserId
        public int UserId { get; set; }
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        // Переименованы FirstName и LastName в Firstname и Lastname
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();


        // Новый параметр Middlename (Отчество)
        public string? Middlename { get; set; } = null;

        // Переименовано DateOfBirth в BirthDate
        public DateTime BirthDate { get; set; }

        // Логин пользователя
        public string Login { get; set; } = null!;

        // Новый способ для хранения email
        public string Email { get; set; } = null!;

        // Новый способ для хранения пароля
        public string Password { get; set; } = null!;

        // Поле для согласия с условиями
        public bool AcceptTerms { get; set; }

        // Новый тип роли пользователя
        public Role Role { get; set; }

        // Параметры для обработки токенов подтверждения и восстановления пароля
        public string? VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public string Gender { get; set; } = null!;

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }

        // Даты для отслеживания создания/обновления пользователя
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        // Идентификатор сотрудника
        public int? EmployeeId { get; set; }

        // Механизм токенов обновлений
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        // Не отображается в БД, считается вычисляемым свойством
        [NotMapped]
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

        // Проверка наличия токена
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
        public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
        public virtual ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
        public virtual ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();
        public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

        public virtual ICollection<ChronicCondition> ChronicConditions { get; set; } = new List<ChronicCondition>();
        public virtual ICollection<DietPlan> DietPlans { get; set; } = new List<DietPlan>();
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
        public virtual ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();
        public virtual ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public virtual ICollection<MedicalTest> MedicalTests { get; set; } = new List<MedicalTest>();

        public virtual ICollection<PhysicalActivity> PhysicalActivities { get; set; } = new List<PhysicalActivity>();
        


       
        // Навигационное свойство для Employee (связь с сотрудником)
        public virtual Employee? Employee { get; set; }
    }
}
