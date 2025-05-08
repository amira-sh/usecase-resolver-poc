namespace UCMediator.Contracts
{
    public interface ISubscriptionStore<TSubscriber, TKey>
    {
        public TSubscriber? this[TKey keyType]
        {
            get;
        }

        public void Subscribe(TSubscriber type, TKey keyType);

        public List<TSubscriber> GetSubscribers();
    }
}
