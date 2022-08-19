using Bank.Account.Application.Helpers;
using Bank.Account.Application.Interfaces;
using Bank.Account.Application.Services;
using Bank.Account.Persistence;

namespace Bank.Account.Api.Configuration
{
    internal static class ServicesConfigurator
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAccountService, AccountService>();
        }
    }
}
