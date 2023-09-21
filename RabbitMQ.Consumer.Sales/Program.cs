using RabbitMQ.Service;
using RabbitMQ.Service.ExchangeConnection;
using RabbitMQ.Service.Factory;

RabbitMQFactory rabbitMQFactory = new RabbitMQFactory();
IExchangeConnection connection = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Headers);
connection.SubscribeTo(ProcessMessage, RabbitMQQeueNames.Sales, "", new Dictionary<string, object> { { "closed", false } });

Console.ReadKey();

void ProcessMessage(string rabbitMQModel)
{
    Console.WriteLine(rabbitMQModel);
}