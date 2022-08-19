using Bank.Transaction.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Bank.Transaction.Api.Configuration
{
    internal static class DatabaseServiceConfigurator
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TransactionDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
