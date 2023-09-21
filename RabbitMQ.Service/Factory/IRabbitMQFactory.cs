using RabbitMQ.Service.ExchangeConnection;

namespace RabbitMQ.Service.Factory
{
    public interface IRabbitMQFactory
    {
        public IExchangeConnection CreateConnection(ExchangeConnectionTypeEnum exchangeConnectionType);
    }
}
