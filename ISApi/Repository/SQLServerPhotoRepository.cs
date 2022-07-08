using ISApi.Model;
using ISApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace ISApi.Repository
{
    public class SQLServerPhotoRepository : IRepository<Photo>
    {
        private ApplicationDbContext _context;
        public SQLServerPhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Photo entity)
        {
            _context.Photos.Add(entity);
        }

        public async Task AddAsync(Photo entity)
        {
            await _context.Photos.AddAsync(entity);
        }
        public Photo Get(Expression<Func<Photo, bool>> forWhere, IEnumerable<Expression<Func<Photo, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return _context.Photos.Where(forWhere).FirstOrDefault();
            }
            return _context.Photos.Where(forWhere).IncludeMultiple(forInclude.ToArray()).FirstOrDefault();
        }
        public async Task<Photo> GetAsync(Expression<Func<Photo, bool>> forWhere, IEnumerable<Expression<Func<Photo, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return await _context.Photos.Where(forWhere).FirstOrDefaultAsync();
            }
            return await _context.Photos.Where(forWhere).IncludeMultiple(forInclude.ToArray()).FirstOrDefaultAsync();
        }

        public IEnumerable<Photo> GetAll(IEnumerable<Expression<Func<Photo, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return _context.Photos.ToList();
            }
            return _context.Photos.IncludeMultiple(forInclude.ToArray()).ToList();
        }
        public async Task<IEnumerable<Photo>> GetAllAsync(IEnumerable<Expression<Func<Photo, object>>> forInclude = null)
        {
            if (forInclude == null)
            {
                return await _context.Photos.ToListAsync();
            }
            return await _context.Photos.IncludeMultiple(forInclude.ToArray()).ToListAsync();
        }
        public void Remove(Photo entity)
        {
            _context.Photos.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Photo entity)
        {
            _context.Update(entity);
        }
    }
}
