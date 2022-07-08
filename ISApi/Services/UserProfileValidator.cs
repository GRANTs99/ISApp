using FluentValidation;
using ISApi.Model.ViewModel;

namespace ISApi.Services
{
    public class UserProfileValidator : AbstractValidator<UserProfileViewModel>
    {
        private readonly string NamePattern = @"[а-яА-ЯёЁa-zA-Z0-9]{2,20}";
        private readonly string AboutPattern = @"[а-яА-ЯёЁa-zA-Z0-9]{1,200}";
        public UserProfileValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Matches(NamePattern).WithMessage("{PropertyName} must be between 2 and 20 characters long");
            RuleFor(p => p.Age).NotEmpty().WithMessage("{PropertyName} must be not empty");
            RuleFor(p => p.About).NotEmpty().Matches(AboutPattern).WithMessage("{PropertyName} must be between 3 and 200 characters long");
        }
    }
}
