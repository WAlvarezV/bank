using Bank.Client.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Bank.Client.Api.Configuration
{
    internal static class DatabaseServiceConfigurator
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ClientDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
