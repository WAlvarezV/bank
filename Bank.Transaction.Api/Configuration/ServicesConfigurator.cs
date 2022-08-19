using Bank.Account.Application.Helpers;
using Bank.Transaction.Application.Interfaces;
using Bank.Transaction.Application.Services;
using Bank.Transaction.Persistence;

namespace Bank.Transaction.Api.Configuration
{
    internal static class ServicesConfigurator
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
        }
    }
}
