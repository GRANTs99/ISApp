using ISApi.Model;
using ISApi.Repository;
using ISApi.Services;

namespace ISApi.Services
{
    public class PhotoService
    {
        private readonly AppUserService _userService;
        private readonly IRepository<Photo> _context;
        public PhotoService(IRepository<Photo> context, AppUserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public async Task<IEnumerable<Photo>> FindUserPhotosAsync(string username)
        {
            var user = await _userService.FindByNameWithPhotoAsync(username);
            if (user != null)
            {
                return user.Photo;
            }
            return null;
        }
        public async Task<Photo> FindUserPhotosByIdAsync(string username, int id)
        {
            var user = await _userService.FindByNameWithPhotoAsync(username);
            if (user != null)
            {
                return user.Photo.Where(p => p.Id == id).FirstOrDefault();
            }
            return null;
        }
        public async Task<bool> SavePhoto(IFormFile photo, AppUser user)
        {
            if (photo != null && user != null)
            {
                var result = CreatePhoto(photo, user);
                if (result != null)
                {
                    _context.Add(result);
                    await _context.SaveAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task DeletePhoto(Photo photo)
        {
            _context.Remove(photo);
            await _context.SaveAsync();
        }
        private Photo CreatePhoto(IFormFile photo, AppUser user)
        {
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(photo.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)photo.Length);
            }
            return new Photo { AppUser = user, Data = imageData, FileName = photo.FileName };
        }
    }
}
