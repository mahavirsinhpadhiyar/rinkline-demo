using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace RinkLine.EntityFrameworkCore
{
    public static class RinkLineDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RinkLineDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RinkLineDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
