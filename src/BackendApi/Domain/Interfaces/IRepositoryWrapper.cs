using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IMedicalRecordRepository MedicalRecord { get; } 
        Task Save();
      
    }
}
