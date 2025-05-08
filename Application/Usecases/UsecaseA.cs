using Domain.Models.UsecaseA;
using UCMediator.Contracts;

namespace Application.Usecases
{
    internal class UsecaseA : IUsecaseAsync<UsecaseAResponse, UsecaseARequest>
    {
        public Task<UsecaseAResponse> ExecuteAsync(UsecaseARequest usecaseRequest)
        {
            return Task.FromResult(new UsecaseAResponse("UsecaseA executed successfully"));
        }
    }
}
