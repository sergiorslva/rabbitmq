using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Service;
using RabbitMQ.Service.ExchangeConnection;
using RabbitMQ.Service.Factory;

namespace RabbitMQ.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        public PublisherController()
        {
        }

        [HttpPost]
        [Route("fanout")]
        public IActionResult Fanout([FromBody] string message)
        {
            RabbitMQFactory rabbitMQFactory = new RabbitMQFactory();
            IExchangeConnection connection = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Fanout);            
            connection.Enqueue(message, new List<RabbitMQQeueNames> { RabbitMQQeueNames.Sales, RabbitMQQeueNames.PCP });            

            return Ok();            
        }

        [HttpPost]
        [Route("direct")]
        public IActionResult Direct([FromBody] string message, string routingKey)
        {
            RabbitMQFactory rabbitMQFactory = new RabbitMQFactory();
            IExchangeConnection connection = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Direct);
            connection.Enqueue(message, null, routingKey);

            return Ok();
        }

        [HttpPost]
        [Route("topic")]
        public IActionResult Topic([FromBody] string message, string routingKey)
        {
            RabbitMQFactory rabbitMQFactory = new RabbitMQFactory();
            IExchangeConnection connection = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Topic);
            connection.Enqueue(message, null, routingKey);

            return Ok();
        }


        [HttpPost]
        [Route("headers")]
        public IActionResult Headers([FromBody] string message)
        {
            RabbitMQFactory rabbitMQFactory = new RabbitMQFactory();
            IExchangeConnection connection = rabbitMQFactory.CreateConnection(ExchangeConnectionTypeEnum.Headers);            
            connection.Enqueue(message, null, "", new Dictionary<string, object> { { "closed", true } });

            return Ok();
        }
    }
}