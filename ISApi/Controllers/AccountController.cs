using IdentityModel.Client;
using ISApi.Model;
using ISApi.Model.ViewModel;
using ISApi.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISApi.Services;
using System.Security.Claims;

namespace ISApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        public AccountController(AppUserService userservice, IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
            _userService = userservice;
        }
        [HttpPost]
        public async Task<IActionResult> Post(RegistrationViewModel model) //Registration
        {
            var user = await _userService.FindByNameAsync(model.UserName);
            if (user == null)
            {
                var result = await _userService.RegistrationUser(model.UserName, model.Password);
                if (result)
                {
                    return Ok();
                }
                return BadRequest();//failed to register
            }
            return BadRequest();//the user already exists
        }
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userService.FindByNameAsync(username);
            var res = await _userService.DeleteUserAsync(user);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
