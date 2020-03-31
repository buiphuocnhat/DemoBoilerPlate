using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Project1.Authorization.Roles;
using Project1.Authorization.Users;
using Project1.MultiTenancy;
using Project1.Entities;

namespace Project1.EntityFrameworkCore
{
    public class Project1DbContext : AbpZeroDbContext<Tenant, Role, User, Project1DbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Car> Cars { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public Project1DbContext(DbContextOptions<Project1DbContext> options)
            : base(options)
        {
        }
    }
}
