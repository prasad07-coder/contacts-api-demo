using FluentValidation;
using UserContactsApi.Dtos;

namespace UserContactsApi.Validators
{
    public class ContactValidator : AbstractValidator<ContactDto>
    {
        public ContactValidator()
        {
            RuleFor(contact => contact.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(contact => contact.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(contact => contact.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

            RuleFor(contact => contact.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");
        }
    }
}
