using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly MedicalContext _context;

        public MedicalRecordRepository(MedicalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalRecord>> FindAll()
        {
            return await _context.MedicalRecords.ToListAsync();  // Получаем все записи
        }

        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords
                                 .FirstOrDefaultAsync(record => record.RecordId == id);  // Поиск записи по ID
        }

        public async Task<IEnumerable<MedicalRecord>> FindByCondition(Expression<Func<MedicalRecord, bool>> expression)
        {
            return await _context.MedicalRecords.Where(expression).ToListAsync();
        }

        public async Task Create(MedicalRecord entity)
        {
            await _context.MedicalRecords.AddAsync(entity);  // Добавление новой записи
        }

        public async Task Update(MedicalRecord entity)
        {
            _context.MedicalRecords.Update(entity);  // Обновление записи
        }

        public async Task Delete(MedicalRecord entity)
        {
            _context.MedicalRecords.Remove(entity);  // Удаление записи
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();  // Сохранение изменений
        }
    }
}
