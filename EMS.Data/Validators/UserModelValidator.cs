using EMS.Data.Entities;
using FluentValidation;

namespace EMS.Data.Validators
{
    public class UserModelValidator : AbstractValidator<User>
    {
        public UserModelValidator() 
        {
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("Invalid email format.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
