using AuthServer.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Infra;

public class UserDbContext : IdentityDbContext<ApplicationUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
}
