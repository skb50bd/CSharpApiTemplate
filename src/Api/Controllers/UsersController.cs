using System.IdentityModel.Tokens.Jwt;
using Template.Core.Services;
using Template.Core.Validators;
using Template.Domain.DTOs;
using Template.Domain.Exceptions;
using Template.Domain.InputModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public Task<ActionResult<UserDto>> Get() =>
        _userService
            .GetCurrentUser()
            .MatchAsync(
                user => Ok(user),
                () => Unauthorized()
            );

    [HttpPost("Register")]
    public Task<ActionResult<UserDto>> Register(UserRegistrationModel model) =>
        _userService
            .Register(model)
            .MatchAsync(
                user => CreatedAtAction(nameof(Get), user),
                exception => exception switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    IdentityException ex   => UnprocessableEntity(ex.ToProblemDetails()),
                    _                      => throw StandardErrors.Unreachable
                }
            );

    [HttpPost("Token")]
    public Task<ActionResult<string>> GetToken(LoginModel model) =>
        _userService
            .CreateAccessToken(model)
            .MatchAsync<JwtSecurityToken, string>(
                token =>
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenString  = tokenHandler.WriteToken(token);
                    return Ok(tokenString);
                },
                exception => exception switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    InvalidCredentials     => Unauthorized(),
                    _                      => throw StandardErrors.Unreachable
                }
            );
}