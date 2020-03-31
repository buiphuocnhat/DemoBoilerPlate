using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Project1.EntityFrameworkCore
{
    public static class Project1DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<Project1DbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<Project1DbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
