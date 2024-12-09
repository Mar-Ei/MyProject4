using Task = System.Threading.Tasks.Task;
using BackendApi.Models;


namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task Create(User model);
        Task Update(User model);
        Task Delete(int id);
    }
}
