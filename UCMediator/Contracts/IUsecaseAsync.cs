namespace UCMediator.Contracts
{
    public interface IUsecaseAsync<U, T>
        where U : IUsecaseResponse
        where T : IUsecaseRequest
    {
        public Task<U> ExecuteAsync(T usecaseRequest);
    }
}
