using Domain.Models;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMedicalRecordRepository
    {
        // Метод для получения всех записей
        Task<IEnumerable<MedicalRecord>> FindAll();

        // Метод для получения записи по ID
        Task<MedicalRecord> GetByIdAsync(int id);

        // Метод для поиска записей по условию
        Task<IEnumerable<MedicalRecord>> FindByCondition(Expression<Func<MedicalRecord, bool>> expression);

        // Метод для создания записи
        Task Create(MedicalRecord entity);

        // Метод для обновления записи
        Task Update(MedicalRecord entity);

        // Метод для удаления записи
        Task Delete(MedicalRecord entity);

        // Метод для сохранения изменений
        Task Save();
    }
}
