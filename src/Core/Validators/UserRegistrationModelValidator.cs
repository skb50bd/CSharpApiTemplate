using Template.Data.Repositories;
using Template.Domain.InputModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Template.Core.Validators;

public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
{
    public UserRegistrationModelValidator(IUserRepository repo)
    {
        RuleFor(_ => _.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email address is required")
            .MustAsync(async (email, ctx) =>
            {
                var emailAlreadyExists = 
                    await repo.Table.AnyAsync(_ => _.Email == email, ctx);
                
                return emailAlreadyExists is false;
            }).WithMessage("Email already exists");

        RuleFor(_ => _.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}