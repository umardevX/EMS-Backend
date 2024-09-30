using EMS.Data.Entities;
using FluentValidation;

namespace EMS.Data.Validators
{
    public class EmployeeModelValidator : AbstractValidator<Employee>
    {
        public EmployeeModelValidator()
        {
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(employee => employee.Email)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(employee => employee.PhoneNumber)
                .Matches(@"^\d{10}$").WithMessage("Invalid phone number format.");
        }
    }
}
