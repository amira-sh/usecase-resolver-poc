using UCMediator.Contracts;

namespace UCMediator.Mediator
{
    public class UsecaseMediator(IServiceProvider serviceProvider,
        UsecaseSubscriptionStore usecaseSubscriptionStore) : IUCMediator
    {
        public async Task<TResponse> ExecuteAsync<TResponse>(IUsecaseRequest usecaseRequest)
            where TResponse : IUsecaseResponse
        {
            var tUsecaseRequest = usecaseRequest.GetType();
            var tUsecase = usecaseSubscriptionStore[tUsecaseRequest] ??
                throw new Exception($"No usecase found for the request: {tUsecaseRequest.FullName}");

            var service = serviceProvider.GetService(tUsecase) ??
                throw new Exception($"No service registered for the type: {tUsecase.FullName}");

            var methodName = nameof(IUsecaseAsync<IUsecaseResponse, IUsecaseRequest>.ExecuteAsync);
            var executeAsyncMethod = tUsecase.GetMethod(methodName);
            return await (Task<TResponse>)executeAsyncMethod!.Invoke(service, [usecaseRequest])!;
        }
    }
}
