using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQ.Service.ExchangeConnection
{
    public class ExchangeConnectionFanout : RabbitMQService, IExchangeConnection
    {
        private readonly string exchangeName = "fanout.rmq.test";
        public ExchangeConnectionFanout()
        {
            base.Initialize();
        }

        public void Enqueue(object message, IEnumerable<RabbitMQQeueNames> qeueNames, string routingKey = "", Dictionary<string, object> headers = null)
        {
            base.AMPQModel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false);

            string json = JsonSerializer.Serialize(message);
            var messageBody = Encoding.UTF8.GetBytes(json);

            foreach (var qeueName in qeueNames)
            {
                AMPQModel.QueueDeclare(queue: qeueName.ToString(), durable: false, exclusive: false, autoDelete: false, arguments: null);
            }

            var properties = AMPQModel.CreateBasicProperties();
            properties.Persistent = false;

            AMPQModel.BasicPublish(exchangeName, string.Empty, true, properties, messageBody);
        }

        public void SubscribeTo(Action<string> callbackMethod, RabbitMQQeueNames qeueName, string routingKey = "", Dictionary<string, object> headers = null)
        {            
            AMPQModel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false);
            AMPQModel.QueueDeclare(queue: qeueName.ToString(), durable: false, exclusive: false, autoDelete: false, arguments: null);
            AMPQModel.QueueBind(qeueName.ToString(), exchangeName, routingKey);

            EventingBasicConsumer consumer = new EventingBasicConsumer(AMPQModel);

            consumer.Received += (sender, e) => Consumer_Received(sender, e, callbackMethod);

            AMPQModel.BasicConsume(qeueName.ToString(), true, consumer);
        }
    }
}
