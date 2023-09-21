using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace RabbitMQ.Service.ExchangeConnection
{
    public class ExchangeConnectionDirect : RabbitMQService, IExchangeConnection
    {
        private readonly string exchangeName = "direct.rmq.test";
        public ExchangeConnectionDirect()
        {
            base.Initialize();
        }
        
        public void Enqueue(object message, IEnumerable<RabbitMQQeueNames> qeueNames, string routingKey = "", Dictionary<string, object> headers = null)
        {
            base.AMPQModel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false);

            string json = JsonSerializer.Serialize(message);
            var messageBody = Encoding.UTF8.GetBytes(json);

            AMPQModel.BasicPublish(exchangeName, routingKey, true, null, messageBody);
        }

        public void SubscribeTo(Action<string> callbackMethod, RabbitMQQeueNames qeueName, string routingKey = "", Dictionary<string, object> headers = null)
        {
            AMPQModel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false);            
            AMPQModel.QueueBind(qeueName.ToString(), exchangeName, routingKey);

            EventingBasicConsumer consumer = new EventingBasicConsumer(AMPQModel);

            consumer.Received += (sender, e) => Consumer_Received(sender, e, callbackMethod);

            AMPQModel.BasicConsume(qeueName.ToString(), true, consumer);
        }
    }
}
