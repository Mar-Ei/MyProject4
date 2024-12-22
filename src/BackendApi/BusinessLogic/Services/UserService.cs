using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<User>> GetAll()
        {
            // Преобразуем IEnumerable<User> в List<User>
            var users = await _repositoryWrapper.User.FindAll();
            return users.ToList();  // Преобразование результата в List
        }

        public async Task<User> GetById(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);
            return user.First();  // Дополнительная проверка на наличие элементов возможна перед First()
        }

        public async Task Create(User model)
        {
            await _repositoryWrapper.User.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(User model)
        {
            _repositoryWrapper.User.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);

            _repositoryWrapper.User.Delete(user.First());
            await _repositoryWrapper.Save();
        }
    }
}
