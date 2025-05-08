using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UCMediator.Contracts;
using UCMediator.Mediator;
using UCMediator.Models;

namespace UCMediator
{
    public static class ServiceCollectionExtension
    {
        public static void AddUCMediator(this IServiceCollection services, List<Assembly> usecaseAssemblies)
        {
            var usecaseSubscriptionStore = new UsecaseSubscriptionStore();
            usecaseSubscriptionStore = SubscribeUsecases(usecaseAssemblies, usecaseSubscriptionStore);
            services.AddSingleton(usecaseSubscriptionStore);

            services.AddScoped<IUCMediator, UsecaseMediator>();

            AddUsecasesToServiceCollection(services, usecaseSubscriptionStore);
        }

        private static UsecaseSubscriptionStore SubscribeUsecases(List<Assembly> usecaseAssemblies,
            UsecaseSubscriptionStore usecaseSubscriptionStore)
        {
            GetUsecaseTypes(usecaseAssemblies)
                .ForEach(u =>
                {
                    var usecaseRequestType = u.TUsecaseGeneric.GetGenericArguments()[1];
                    if (usecaseRequestType == null)
                        throw new Exception($"{nameof(usecaseRequestType)} cannot be resolved!");

                    usecaseSubscriptionStore.Subscribe(u.TUsecase, usecaseRequestType);
                });

            return usecaseSubscriptionStore;
        }

        private static List<UsecaseType> GetUsecaseTypes(List<Assembly> usecaseAssemblies)
        {
            return usecaseAssemblies
                .SelectMany(assembly =>
                    assembly.GetTypes()
                        .Select(usecaseType =>
                        {
                            var iUsecaseAsyncType = usecaseType.GetInterfaces()
                                .FirstOrDefault(t =>
                                    t.IsGenericType &&
                                    t.GetGenericTypeDefinition() == typeof(IUsecaseAsync<,>)
                                );

                            return new UsecaseType(usecaseType, iUsecaseAsyncType!);
                        })
                        .Where((u) => !u.TUsecase.IsAbstract &&
                            !u.TUsecase.IsInterface &&
                            u.TUsecaseGeneric != null
                        )
                )
                .ToList();
        }

        private static void AddUsecasesToServiceCollection(IServiceCollection services,
            UsecaseSubscriptionStore usecaseSubscriptionStore)
        {
            usecaseSubscriptionStore.GetSubscribers()
                .ForEach(u => services.AddScoped(u));
        }
    }
}
