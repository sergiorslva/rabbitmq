using RabbitMQ.Service;
using RabbitMQ.Service.ExchangeConnection;
using RabbitMQ.Service.Factory;

#region Exchange Direct
RabbitMQFactory rabbitMQFactory = new RabbitMQFactory();
IExchangeConnection connectionDirectPCP = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Direct);
connectionDirectPCP.SubscribeTo(ProcessMessageDirectPCP, RabbitMQQeueNames.PCP, "FILIAL.A");

IExchangeConnection connectionDirectSales = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Direct);
connectionDirectSales.SubscribeTo(ProcessMessageDirectSales, RabbitMQQeueNames.Sales, "FILIAL.A");

void ProcessMessageDirectPCP(string rabbitMQModel)
{
    Console.WriteLine($"Processing Direct (PCP): {rabbitMQModel}");
}

void ProcessMessageDirectSales(string rabbitMQModel)
{
    Console.WriteLine($"Processing Direct (Sales): {rabbitMQModel}");
}
#endregion

#region Exchange Topic
IExchangeConnection connectionTopicPCP = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Topic);
connectionTopicPCP.SubscribeTo(ProcessMessageTopicPCP, RabbitMQQeueNames.PCP, "FILIAL.*");

IExchangeConnection connectionTopicSales = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Topic);
connectionTopicSales.SubscribeTo(ProcessMessageTopicSales, RabbitMQQeueNames.Sales, "FILIAL.*");
void ProcessMessageTopicPCP(string rabbitMQModel)
{
    Console.WriteLine($"Processing Topic (PCP): {rabbitMQModel}");
}

void ProcessMessageTopicSales(string rabbitMQModel)
{
    Console.WriteLine($"Processing Topic (Sales): {rabbitMQModel}");
}
#endregion

#region Exchange Fanout
IExchangeConnection connectionFanoutPCP = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Fanout);
connectionFanoutPCP.SubscribeTo(ProcessMessageFanoutPCP, RabbitMQQeueNames.PCP);

IExchangeConnection connectionFanoutSales = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Fanout);
connectionFanoutSales.SubscribeTo(ProcessMessageFanoutSales, RabbitMQQeueNames.Sales);
void ProcessMessageFanoutPCP(string rabbitMQModel)
{
    Console.WriteLine($"Processing Fanout (PCP): {rabbitMQModel}");
}

void ProcessMessageFanoutSales(string rabbitMQModel)
{
    Console.WriteLine($"Processing Fanout (Sales): {rabbitMQModel}");
}
#endregion

#region Exchange Headers
IExchangeConnection connectionHeadersPCP = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Headers);
connectionHeadersPCP.SubscribeTo(ProcessMessageHeadersPCP, RabbitMQQeueNames.PCP, "", new Dictionary<string, object> { { "FILIAL", "A"} });

IExchangeConnection connectionHeadersSales = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Headers);
connectionHeadersSales.SubscribeTo(ProcessMessageHeadersSales, RabbitMQQeueNames.Sales, "", new Dictionary<string, object> { { "FILIAL", "B"} });
void ProcessMessageHeadersPCP(string rabbitMQModel)
{
    Console.WriteLine($"Processing Headers (PCP): {rabbitMQModel}");
}

void ProcessMessageHeadersSales(string rabbitMQModel)
{
    Console.WriteLine($"Processing Headers (Sales): {rabbitMQModel}");
}
#endregion
Console.ReadKey();



