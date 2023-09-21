using RabbitMQ.Service.ExchangeConnection;

namespace RabbitMQ.Service.Factory
{
    public class RabbitMQFactory : IRabbitMQFactory
    {
        public IExchangeConnection CreateConnection(ExchangeConnectionTypeEnum exchangeConnectionType)
        {
            switch (exchangeConnectionType)
            {
                case ExchangeConnectionTypeEnum.Topic:
                    {
                        return new ExchangeConnectionTopic();
                    }
                case ExchangeConnectionTypeEnum.Direct:
                    {
                        return new ExchangeConnectionDirect();
                    }
                case ExchangeConnectionTypeEnum.Fanout:
                    {
                        return new ExchangeConnectionFanout();
                    }
                case ExchangeConnectionTypeEnum.Headers:
                    {
                        return new ExchangeConnectionHeaders();
                    }
                default:
                    {
                        throw new ArgumentException("Invalid exchange type");
                    }
            }
        }
    }
}
