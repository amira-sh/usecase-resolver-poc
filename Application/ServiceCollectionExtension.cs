using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UCMediator;

namespace Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddUCMediator([Assembly.GetExecutingAssembly()]);
        }
    }
}
