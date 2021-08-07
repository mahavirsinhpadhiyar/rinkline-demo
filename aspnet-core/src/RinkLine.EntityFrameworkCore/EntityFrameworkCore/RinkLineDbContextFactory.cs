using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RinkLine.Configuration;
using RinkLine.Web;

namespace RinkLine.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class RinkLineDbContextFactory : IDesignTimeDbContextFactory<RinkLineDbContext>
    {
        public RinkLineDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RinkLineDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            RinkLineDbContextConfigurer.Configure(builder, configuration.GetConnectionString(RinkLineConsts.ConnectionStringName));

            return new RinkLineDbContext(builder.Options);
        }
    }
}
