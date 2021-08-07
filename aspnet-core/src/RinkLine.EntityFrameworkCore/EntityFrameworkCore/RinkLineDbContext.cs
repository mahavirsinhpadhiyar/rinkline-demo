using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using RinkLine.Authorization.Roles;
using RinkLine.Authorization.Users;
using RinkLine.MultiTenancy;

namespace RinkLine.EntityFrameworkCore
{
    public class RinkLineDbContext : AbpZeroDbContext<Tenant, Role, User, RinkLineDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public RinkLineDbContext(DbContextOptions<RinkLineDbContext> options)
            : base(options)
        {
        }
    }
}
