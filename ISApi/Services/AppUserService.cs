using ISApi.Model;
using ISApi.Repository;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace ISApi.Services
{
    public class AppUserService
    {
        private readonly IRepository<AppUser> _context;
        public AppUserService(IRepository<AppUser> context)
        {
            _context = context;
        }
        public AppUser FindByName(string username)
        {
            return _context.Get(p => p.UserName == username);
        }
        public async Task<AppUser> FindByNameAsync(string username)
        {
            return await _context.GetAsync(p => p.UserName == username);
        }
        public async Task<AppUser> FindByNameWithPhotoAsync(string username)
        {
            return await _context.GetAsync(p => p.UserName == username, new List<Expression<Func<AppUser, object>>>() { p => p.Photo});
        }
        public async Task<AppUser> FindByNameWithProfileAsync(string username)
        {
            return await _context.GetAsync(p => p.UserName == username, new List<Expression<Func<AppUser, object>>>() { p => p.Profile});
        }
        public async Task<AppUser> FindByNameWithPhotoAndProfileAsync(string username)
        {
            return await _context.GetAsync(p => p.UserName == username, new List<Expression<Func<AppUser, object>>>() { p => p.Profile, p => p.Photo });
        }
        public bool SingInUser(AppUser user, string password)
        {
            var u = FindByName(user.UserName);
            if (u != null)
            {
                if (u.PasswordHash == HashPassword(password))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> RegistrationUser(string username, string password)
        {
            var u = await FindByNameAsync(username);
            if (u == null)
            {
                var user = new AppUser { UserName = username, PasswordHash = HashPassword(password) };
                _context.Add(user);
                await _context.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteUserAsync(AppUser user)
        {
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveAsync();
                return true;
            }
            return false;
        }
        public string HashPassword(string password)
        {
            byte[] data = Encoding.Default.GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            return Convert.ToBase64String(result);
        }
    }
}