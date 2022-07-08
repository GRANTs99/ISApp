using ISApi.Model.ViewModel;
using ISApi.Model;
using ISApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISApi.Services;
using System.Security.Claims;
using ISApi.Services;

namespace ISApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly AppUserService _userService;
        private readonly PhotoService _photoService;
        private readonly IHttpContextAccessor _httpContext;
        public PhotoController(IHttpContextAccessor httpContextAccessor, PhotoService photoService, IRepository<Photo> context, AppUserService userService)
        {
            _userService = userService;
            _photoService = photoService;
            _httpContext = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string username, int id = -1)
        {
            var photo = await _photoService.FindUserPhotosAsync(username);
            if (photo != null)
            {
                if (id != -1)
                {
                    return Ok(photo.Where(p => p.Id == id).FirstOrDefault().Data);
                }
                var photodata = new List<byte[]>();
                foreach(var p in photo)
                {
                    photodata.Add(p.Data);
                }
                return Ok(photodata);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile photo)
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _user = await _userService.FindByNameAsync(username);
            if (_user != null)
            {
                var result = await _photoService.SavePhoto(photo, _user);
                if (result)
                {
                    return Ok();
                }
            }
            return BadRequest();

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _user = await _userService.FindByNameWithPhotoAsync(username);
            if (_user != null)
            {
                var photo = _user.Photo.Where(p => p.Id == id).FirstOrDefault();
                if (photo != null)
                {
                    await _photoService.DeletePhoto(photo);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
