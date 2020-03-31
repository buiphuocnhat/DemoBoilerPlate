using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project1.Configuration;
using Project1.Web;

namespace Project1.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class Project1DbContextFactory : IDesignTimeDbContextFactory<Project1DbContext>
    {
        public Project1DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Project1DbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            Project1DbContextConfigurer.Configure(builder, configuration.GetConnectionString(Project1Consts.ConnectionStringName));

            return new Project1DbContext(builder.Options);
        }
    }
}
