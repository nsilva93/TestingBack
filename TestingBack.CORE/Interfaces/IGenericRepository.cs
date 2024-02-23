using System.Linq.Expressions;

namespace TestingBack.CORE.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<bool> Add(T entity);
        Task<bool> Delete(int id);
        Task<bool> Update(T entity);
    }
}
