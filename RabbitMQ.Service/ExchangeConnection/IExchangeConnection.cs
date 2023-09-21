namespace RabbitMQ.Service.ExchangeConnection
{
    public interface IExchangeConnection
    {
        void Enqueue(object message, IEnumerable<RabbitMQQeueNames> qeueNames, string routingKey = "", Dictionary<string, object> headers = null);
        void SubscribeTo(Action<string> callbackMethod, RabbitMQQeueNames qeueName, string routingKey = "", Dictionary<string, object> headers = null);
    }
}
