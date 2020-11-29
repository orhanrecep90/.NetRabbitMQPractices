using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = int.Parse(args[1]);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("your amqp url");
            //factory.HostName = "localhost";

            using (var connection=factory.CreateConnection())
            {   
                using (var channel=connection.CreateModel())
                {
                    channel.QueueDeclare("queue_1", true, false, false, null);


                    for (int i = 1; i <= count; i++)
                    {
                        string message = $"{args[0].ToString()} --- {i}";
                        var bodyByte = Encoding.UTF8.GetBytes($"{message}");

                        var properties = channel.CreateBasicProperties();

                        properties.Persistent = true;

                        channel.BasicPublish("", "queue_1", properties, bodyByte);

                        Console.WriteLine($"{message}  * send!");
                    }
                }
            }
        }
    }
}



