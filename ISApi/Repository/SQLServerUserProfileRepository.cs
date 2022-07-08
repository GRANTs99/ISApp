using ISApi.Model;
using ISApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ISApi.Repository
{
    public class SQLServerUserProfileRepository : IRepository<UserProfile>
    {
        private ApplicationDbContext _context;
        public SQLServerUserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(UserProfile entity)
        {
            _context.UserProfiles.Add(entity);
        }

        public async Task AddAsync(UserProfile entity)
        {
            await _context.UserProfiles.AddAsync(entity);
        }

        public UserProfile Get(Expression<Func<UserProfile, bool>> forWhere, IEnumerable<Expression<Func<UserProfile, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return _context.UserProfiles.Where(forWhere).FirstOrDefault();
            }
            return _context.UserProfiles.Where(forWhere).IncludeMultiple(forInclude.ToArray()).FirstOrDefault();
        }
        public async Task<UserProfile> GetAsync(Expression<Func<UserProfile, bool>> forWhere, IEnumerable<Expression<Func<UserProfile, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return await _context.UserProfiles.Where(forWhere).FirstOrDefaultAsync();
            }
            return await _context.UserProfiles.Where(forWhere).IncludeMultiple(forInclude.ToArray()).FirstOrDefaultAsync();
        }

        public IEnumerable<UserProfile> GetAll(IEnumerable<Expression<Func<UserProfile, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return _context.UserProfiles.ToList();
            }
            return _context.UserProfiles.IncludeMultiple(forInclude.ToArray()).ToList();
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync(IEnumerable<Expression<Func<UserProfile, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return await _context.UserProfiles.ToListAsync();
            }
            return await _context.UserProfiles.IncludeMultiple(forInclude.ToArray()).ToListAsync();
        }
        public void Remove(UserProfile entity)
        {
            _context.UserProfiles.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(UserProfile entity)
        {
            _context.Update(entity);
        }
    }
}
