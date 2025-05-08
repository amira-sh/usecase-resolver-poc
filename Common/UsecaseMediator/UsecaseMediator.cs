using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Common.UsecaseMediator
{
    public class UsecaseMediator(IServiceProvider serviceProvider) : IUsecaseMediator
    {
        public async Task<TResponse> ExecuteAsync<TResponse>(IUsecaseRequest usecaseRequest)
            where TResponse : IUsecaseResponse
        {
            var services = serviceProvider.GetServices(typeof(IUsecase));
            if (services == null || !services.Any())
                throw new Exception("No Usecase registered!");

            var requestType = usecaseRequest.GetType();
            foreach (var service in services)
            {
                if (service == null) continue;

                var serviceType = service?.GetType();
                var iUsecaseAsyncType = serviceType?
                    .GetInterfaces()
                    .FirstOrDefault(u => u.IsGenericType && u.GetGenericTypeDefinition() == typeof(IUsecaseAsync<,>));
                
                if (iUsecaseAsyncType == null) continue;

                if (iUsecaseAsyncType.GetGenericArguments()[1] != requestType) continue;
                
                var methodName = nameof(IUsecaseAsync<IUsecaseResponse, IUsecaseRequest>.ExecuteAsync);
                var executeAsyncMethod = serviceType!.GetMethod(methodName);
                return await (Task<TResponse>)executeAsyncMethod!.Invoke(service, [usecaseRequest])!;
            }

            throw new Exception($"Usecase not found for request type {requestType}");
        }
    }
}
