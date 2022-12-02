using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Template.Domain.Exceptions;

public static class StandardErrors
{
    public static InvalidCredentials InvalidCredentials { get; } = new();
    public static UnreachableException Unreachable { get; } = new();
}

public class InvalidCredentials : Exception { }

public class IdentityException : Exception
{
    public List<IdentityError> Errors { get; private set; }

    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors.ToList();
    }
}