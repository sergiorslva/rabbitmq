using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQ.Service.ExchangeConnection
{
    public class ExchangeConnectionHeaders : RabbitMQService, IExchangeConnection
    {
        private readonly string exchangeName = "headers.rmq.test";
        public ExchangeConnectionHeaders()
        {
            base.Initialize();
        }

        public void Enqueue(object message, IEnumerable<RabbitMQQeueNames> qeueNames, string messageGroup = "", Dictionary<string, object> headers = null)
        {
            base.AMPQModel.ExchangeDeclare(exchangeName, ExchangeType.Headers, true, false);

            string json = JsonSerializer.Serialize(message);
            var messageBody = Encoding.UTF8.GetBytes(json);

            var properties = AMPQModel.CreateBasicProperties();
            properties.Persistent = false;
            properties.Headers = headers;

            AMPQModel.BasicPublish(exchangeName, string.Empty, true, properties, messageBody);
        }

        public void SubscribeTo(Action<string> callbackMethod, RabbitMQQeueNames qeueName, string routingKey = "", Dictionary<string, object> headers = null)
        {
            AMPQModel.ExchangeDeclare(exchangeName, ExchangeType.Headers, true, false);
            AMPQModel.QueueBind(qeueName.ToString(), exchangeName, routingKey, headers);
            
            EventingBasicConsumer consumer = new EventingBasicConsumer(AMPQModel);

            consumer.Received += (sender, e) => Consumer_Received(sender, e, callbackMethod);

            AMPQModel.BasicConsume(qeueName.ToString(), true, consumer);
        }
    }
}
