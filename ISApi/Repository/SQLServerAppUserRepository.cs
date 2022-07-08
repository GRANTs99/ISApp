using ISApi.Model;
using ISApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace ISApi.Repository
{
    public class SQLServerAppUserRepository : IRepository<AppUser>
    {
        private ApplicationDbContext _context;
        public SQLServerAppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(AppUser entity)
        {
            _context.AppUsers.Add(entity);
        }

        public async Task AddAsync(AppUser entity)
        {
            await _context.AppUsers.AddAsync(entity);
        }

        public AppUser Get(Expression<Func<AppUser, bool>> forWhere, IEnumerable<Expression<Func<AppUser, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return _context.AppUsers.Where(forWhere).FirstOrDefault();
            }
            return _context.AppUsers.Where(forWhere).IncludeMultiple(forInclude.ToArray()).FirstOrDefault();
        }
        public async Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> forWhere, IEnumerable<Expression<Func<AppUser, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return await _context.AppUsers.Where(forWhere).FirstOrDefaultAsync();
            }
            return await _context.AppUsers.Where(forWhere).IncludeMultiple(forInclude.ToArray()).FirstOrDefaultAsync();
        }

        public IEnumerable<AppUser> GetAll(IEnumerable<Expression<Func<AppUser, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return _context.AppUsers.ToList();
            }
            return _context.AppUsers.IncludeMultiple(forInclude.ToArray()).ToList();
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync(IEnumerable<Expression<Func<AppUser, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return await _context.AppUsers.ToListAsync();
            }
            return await _context.AppUsers.IncludeMultiple(forInclude.ToArray()).ToListAsync();
        }

        public void Remove(AppUser entity)
        {
            _context.AppUsers.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(AppUser item)
        {
            _context.Update(item);
        }
    }
}
