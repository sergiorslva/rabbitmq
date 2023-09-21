using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Service
{
    public abstract class RabbitMQService
    {        
        public IModel AMPQModel = null;
        public IConnection Connection = null;        
          
        public void Initialize()
        {            
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.UserName = "smartadvisor";
            connectionFactory.Password = "@mtFbwy!";
            connectionFactory.VirtualHost = "sadv-vhost";
            connectionFactory.Port = 5672;

            Connection = connectionFactory.CreateConnection();
            AMPQModel = Connection.CreateModel();                            
        }

        public bool IsConnected()
        {
            try
            {
                return Connection.IsOpen;
            }
            catch
            {
                return false;
            }
        }

        public void ResetConnection()
        {            
            if (IsConnected())
            {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
            Initialize();            
        }        

        public void Consumer_Received(object sender, BasicDeliverEventArgs e, Action<string> callbackMethod)
        {
            var body = e.Body.ToArray();
            var jsonResult = Encoding.UTF8.GetString(body);                        
            callbackMethod(jsonResult);                        
        }
    }
}
