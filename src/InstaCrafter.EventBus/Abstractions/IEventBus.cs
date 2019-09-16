using InstaCrafter.EventBus.Messages;

namespace InstaCrafter.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(IntegrationMessage message);

        void Subscribe<T, TH>()
            where T : IntegrationMessage
            where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationMessage;
    }
}
