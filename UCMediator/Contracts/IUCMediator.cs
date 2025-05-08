namespace UCMediator.Contracts
{
    public interface IUCMediator
    {
        public Task<TResponse> ExecuteAsync<TResponse>(IUsecaseRequest usecaseRequest)
            where TResponse : IUsecaseResponse;
    }
}
