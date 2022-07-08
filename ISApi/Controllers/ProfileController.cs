using ISApi.Model;
using ISApi.Model.ViewModel;
using ISApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISApi.Services;
using System.Security.Claims;

namespace ISApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IRepository<UserProfile> _context;
        private readonly AppUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        public ProfileController(IHttpContextAccessor httpContextAccessor, AppUserService userService, IRepository<UserProfile> context)
        {
            _context = context;
            _userService = userService;
            _httpContext = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string username) //get profile
        {
            var user = await _userService.FindByNameWithProfileAsync(username);
            if (user != null)
            {
                var profile = user.Profile;
                if (profile != null)
                {
                    var p = new UserProfileViewModel { About = profile.About, Age = profile.Age, Name = profile.Name };
                    return Ok(p);
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserProfileViewModel model) //set profile
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _user = await _userService.FindByNameWithProfileAsync(username);
            if (_user != null && _user.Profile == null)
            {
                var profile = new UserProfile { Name = model.Name, Age = model.Age, About = model.About, UserName = _user.UserName, AppUser = _user };
                _context.Add(profile);
                await _context.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete() //delete profile
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _user = await _userService.FindByNameWithProfileAsync(username);
            if (_user != null && _user.Profile != null)
            {
                _context.Remove(_user.Profile);
                await _context.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(UserProfileViewModel model)
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _user = await _userService.FindByNameWithProfileAsync(username);
            if (_user != null && _user.Profile != null)
            {
                var profile = new UserProfile { Name = model.Name, Age = model.Age, About = model.About, UserName = _user.UserName, AppUser = _user };
                _context.Update(profile);
                await _context.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
