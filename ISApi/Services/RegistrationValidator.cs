using FluentValidation;
using ISApi.Model.ViewModel;

namespace ISApi.Services
{
    public class RegistrationValidator : AbstractValidator<RegistrationViewModel>
    {
        private readonly string UserNamePattern = @"[a-zA-Z\d]{6,20}";
        private readonly string PasswordPattern = @"((?=.\d)(?=.*[a-z])(?=.*[A-Z]).{6,20})";
        public RegistrationValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().Matches(UserNamePattern).WithMessage("{PropertyName} must be between 6 and 20 characters long");
            RuleFor(p => p.Password).NotEmpty().Matches(PasswordPattern).WithMessage("{PropertyName} must be between 6 and 20 characters long must contain at least one number  a lowercase letter  an uppercase letter");
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword).WithMessage("{PropertyName} don't match");
        }
    }
}
