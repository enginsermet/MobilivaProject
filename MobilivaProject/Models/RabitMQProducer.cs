using MobilivaProject.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MobilivaProject.Models
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendOrderMessage<T>(T message)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using
            var channel = connection.CreateModel();

            channel.QueueDeclare("order", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "order", body: body);
        }
    }
}
