using Domain.Models;
using Domain.Interfaces;
using DataAccess.Repositories;
using Task = System.Threading.Tasks.Task;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MedicalContext _repoContext;
        private IUserRepository _user;
        private IMedicalRecordRepository _medicalRecord;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IMedicalRecordRepository MedicalRecord
        {
            get
            {
                if (_medicalRecord == null)
                {
                    _medicalRecord = new MedicalRecordRepository(_repoContext);
                }
                return _medicalRecord;
            }
        }


        public RepositoryWrapper(MedicalContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task Save()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
