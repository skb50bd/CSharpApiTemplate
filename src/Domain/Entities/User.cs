using Microsoft.AspNetCore.Identity;

namespace Template.Domain.Entities;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public override Guid Id { get; set; }
    public required string FullName { get; set; }
    public DateTimeOffset JoinedOn { get; private set; } = DateTimeOffset.UtcNow;
}