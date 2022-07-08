using IdentityServer4.Models;
using IdentityServer4.Validation;
using ISApi.Services;

namespace IdentityServerAuth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly AppUserService _userContext;
        public ResourceOwnerPasswordValidator(AppUserService userContext)
        {
            _userContext = userContext;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _userContext.FindByNameAsync(context.UserName);
                if (user != null)
                {
                    if (user.PasswordHash == _userContext.HashPassword(context.Password))
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.UserName,
                            authenticationMethod: "custom");
                        return;
                    }
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
    }
}
