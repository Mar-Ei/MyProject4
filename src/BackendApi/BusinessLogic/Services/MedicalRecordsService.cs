using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MedicalRecordsService : IMedicalRecordRepository
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public MedicalRecordsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<MedicalRecord>> FindAll()
        {
            var records = await _repositoryWrapper.MedicalRecord.FindAll();
            return records;  // Возвращаем IEnumerable напрямую
        }



        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            var record = await _repositoryWrapper.MedicalRecord.FindByCondition(r => r.RecordId == id);
            return record.FirstOrDefault(); // Возвращаем первую запись или null
        }

        public async Task Create(MedicalRecord model)
        {
            await _repositoryWrapper.MedicalRecord.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(MedicalRecord model)
        {
            _repositoryWrapper.MedicalRecord.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(MedicalRecord model)
        {
            _repositoryWrapper.MedicalRecord.Delete(model);
            await _repositoryWrapper.Save();
        }

        public async Task Save()
        {
            await _repositoryWrapper.Save();
        }

        // Метод для поиска записей по условию
        public async Task<IEnumerable<MedicalRecord>> FindByCondition(Expression<Func<MedicalRecord, bool>> expression)
        {
            var records = await _repositoryWrapper.MedicalRecord.FindByCondition(expression);
            return records;
        }
    }
}
