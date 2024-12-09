using BackendApi.Models;
using Domain.Interfaces;

namespace DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MedicalContext repositoryContext) 
            : base(repositoryContext) 
        {
        }
    }
}
