using System.Linq.Expressions;

public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> FindAll();
    Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task Save();  // Этот метод может быть наследован от IRepositoryWrapper
}
