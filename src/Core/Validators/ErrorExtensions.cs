using Template.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Core.Validators;

public static class ErrorExtensions
{
    private const string UnprocessableEntityDesc = 
        "https://www.rfc-editor.org/rfc/rfc4918#section-11.2";

    public static ValidationProblemDetails ToProblemDetails(
        this ValidationException exception)
    {
        var error = new ValidationProblemDetails
        {
            Type   = UnprocessableEntityDesc,
            Status = StatusCodes.Status422UnprocessableEntity
        };

        foreach (var validationFailure in exception.Errors)  
        {
            if (error.Errors.ContainsKey(validationFailure.PropertyName))
            {
                error.Errors[validationFailure.PropertyName] =
                    error.Errors[validationFailure.PropertyName]
                        .Concat(new[] { validationFailure.ErrorMessage })
                        .ToArray();
            }
            else
            {
                error.Errors.Add(new(
                    validationFailure.PropertyName, 
                    new[] { validationFailure.ErrorMessage }
                ));
            }
        }

        return error;
    }

    public static ValidationProblemDetails ToProblemDetails(
        this IdentityException exception)
    {
        var error = new ValidationProblemDetails
        {
            Type   = UnprocessableEntityDesc,
            Status = StatusCodes.Status422UnprocessableEntity
        };

        foreach (var identityError in exception.Errors)  
        {
            if (error.Errors.ContainsKey(identityError.Code))
            {
                error.Errors[identityError.Code] =
                    error.Errors[identityError.Code]
                        .Concat(new[] { identityError.Description })
                        .ToArray();
            }
            else
            {
                error.Errors.Add(new(
                    identityError.Code, 
                    new[] { identityError.Description }
                ));
            }
        }

        return error;
    }
}