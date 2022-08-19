using Bank.Account.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Bank.Account.Api.Configuration
{
    internal static class DatabaseServiceConfigurator
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AccountDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
