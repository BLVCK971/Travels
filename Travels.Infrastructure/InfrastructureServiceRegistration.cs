using Travels.Application.Contracts.Infrastructure;
using Travels.Application.Models.Mail;
using Travels.Infrastructure.FileExport;
using Travels.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Travels.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IBoardingCardSorter, BoardingCardSorter>();

        return services;
    }
}
