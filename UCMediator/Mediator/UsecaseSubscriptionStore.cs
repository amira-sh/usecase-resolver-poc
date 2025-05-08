using UCMediator.Contracts;

namespace UCMediator.Mediator
{
    public class UsecaseSubscriptionStore() : ISubscriptionStore<Type, Type>
    {
        private readonly Dictionary<string, Type> _usecaseDic = [];

        public Type? this[Type tUsecaseRequest]
        {
            get
            {
                var usecaseRequestName = tUsecaseRequest.FullName ??
                throw new Exception($"Indexer cannot resolve '{nameof(tUsecaseRequest)}' name!");

                return _usecaseDic.TryGetValue(usecaseRequestName, out var usecaseType) ?
                    usecaseType :
                    null;
            }
        }

        public void Subscribe(Type tUsecase, Type tUsecaseRequest)
        {
            var usecaseRequestKey = tUsecaseRequest.FullName ??
                throw new Exception($"The name of type '{nameof(tUsecaseRequest)}' cannot be resolved");

            if (_usecaseDic.ContainsKey(usecaseRequestKey))
                return;
                
            _usecaseDic.Add(usecaseRequestKey, tUsecase);
        }

        public List<Type> GetSubscribers()
        {
            return _usecaseDic.Values.ToList();
        }
    }
}
