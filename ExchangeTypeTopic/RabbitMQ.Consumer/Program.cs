using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ.Consumer
{
    public enum LogNames
    {
        Critical = 1,
        Error = 2,
        Info = 3,
        Warning = 4
    }
    class Program
    {
        static void Main(string[] args)
        {
            int time = int.Parse(args[0].ToString());
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps:");
            //factory.HostName = "localhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare("topic", durable: true, type: ExchangeType.Topic);

                    var queueName = channel.QueueDeclare().QueueName;

                    string routingKey = "Warning.*.Warning";

                    channel.QueueBind(queueName, exchange: "topic", routingKey: routingKey);

                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);

                    channel.BasicConsume(queueName, false, consumer);

                    consumer.Received += (model, x) =>
                    {
                        var message = Encoding.UTF8.GetString(x.Body.ToArray());
                        Console.WriteLine($" {message}  * received");
                        Thread.Sleep(time);
                        channel.BasicAck(x.DeliveryTag, false);

                    };
                    Console.ReadLine();

                }
            }

        }

    }
}


