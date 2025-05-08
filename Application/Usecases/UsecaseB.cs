using Domain.Models.UsecaseB;
using UCMediator.Contracts;

namespace Application.Usecases
{
    internal class UsecaseB : IUsecaseAsync<UsecaseBResponse, UsecaseBRequest>
    {
        public Task<UsecaseBResponse> ExecuteAsync(UsecaseBRequest usecaseRequest)
        {
            return Task.FromResult(new UsecaseBResponse("UsecaseB executed successfully"));
        }
    }
}
