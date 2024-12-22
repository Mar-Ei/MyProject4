using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MedicalContext repositoryContext) : base(repositoryContext) // вызываем конструктор родительского класса
        {
        }

        // Используем _context вместо DbContext
        public async Task<User> GetByIdWithToken(int userId) =>
            await _context.Set<User>().Include(x => x.RefreshTokens).AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<User> GetByEmailWithToken(string email) =>
            await _context.Set<User>().Include(x => x.RefreshTokens).AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }
}
