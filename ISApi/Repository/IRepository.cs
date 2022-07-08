using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace ISApi.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> forWhere, IEnumerable<Expression<Func<T, object>>> forInclude = null);
        Task<T> GetAsync(Expression<Func<T, bool>> forWhere, IEnumerable<Expression<Func<T, object>>> forInclude = null);
        IEnumerable<T> GetAll(IEnumerable<Expression<Func<T, object>>> forInclude = null);
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<Expression<Func<T, object>>> forInclude = null);
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Save();
        Task SaveAsync();

    }
}
