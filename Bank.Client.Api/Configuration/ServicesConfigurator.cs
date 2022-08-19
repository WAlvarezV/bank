using Bank.Client.Application.Helpers;
using Bank.Client.Application.Interfaces;
using Bank.Client.Application.Services;
using Bank.Client.Persistence;

namespace Bank.Client.Api.Configuration
{
    internal static class ServicesConfigurator
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IClientService, ClientService>();
            builder.Services.AddTransient<ICredentialService, CredentialService>();
        }
    }
}
