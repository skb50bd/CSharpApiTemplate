using System.IdentityModel.Tokens.Jwt;
using Template.Domain.DTOs;
using Template.Domain.InputModels;
using LanguageExt.Common;

namespace Template.Core.Services;

public interface IUserService
{
    Task<UserDto?> GetByEmail(string email);
    Task<UserDto?> GetByUserName(string userName);
    Task<Result<UserDto>> Register(UserRegistrationModel model);
    Task<UserDto?> GetCurrentUser();
    Task<Result<JwtSecurityToken>> CreateAccessToken(LoginModel login);
}