using Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Template.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository 
{
    public UserRepository(AppDbContext dbCtx) : base(dbCtx) { }

    public Task<User?> GetByEmail(string email) =>
        Table.FirstOrDefaultAsync(u => u.Email == email);

    public Task<User?> GetByUserName(string userName) =>
        Table.FirstOrDefaultAsync(u => u.UserName == userName);
}