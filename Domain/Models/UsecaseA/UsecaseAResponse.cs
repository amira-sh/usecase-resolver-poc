using UCMediator.Contracts;

namespace Domain.Models.UsecaseA
{
    public record UsecaseAResponse (string Result) : IUsecaseResponse;
}
