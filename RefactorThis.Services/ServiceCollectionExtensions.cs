using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Persistence;

namespace RefactorThis.Services
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      // Register repositories
      services.AddScoped<IInvoiceRepository, InvoiceRepository>();

      // Register services
      services.AddScoped<IInvoiceService, InvoiceService>();

      return services;
    }
  }
}
