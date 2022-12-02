using Template.Domain.Entities;

namespace Template.Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task<User?> GetByUserName(string userName);
}