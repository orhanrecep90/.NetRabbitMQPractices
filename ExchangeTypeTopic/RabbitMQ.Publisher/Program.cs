using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Publisher
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
            //int count = int.Parse(args[1]);
            int count = 20; 
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps:");
            //for local RabbitMq installation
            //factory.HostName = "localhost";

            using (var connection=factory.CreateConnection())
            {   
                using (var channel=connection.CreateModel())
                {
                    channel.ExchangeDeclare("topic", durable:true,type:ExchangeType.Topic);

                    Array logNameArray = Enum.GetValues(typeof(LogNames));

                    for (int i = 1; i <= count; i++)
                    {
                        Random rnd = new Random();

                        LogNames log1 = (LogNames)logNameArray.GetValue(rnd.Next(logNameArray.Length));
                        LogNames log2 = (LogNames)logNameArray.GetValue(rnd.Next(logNameArray.Length));
                        LogNames log3 = (LogNames)logNameArray.GetValue(rnd.Next(logNameArray.Length));

                        string routingKey = $"{log1.ToString()}.{log2.ToString()}.{log3.ToString()}";

                        var bodyByte = Encoding.UTF8.GetBytes($"log={routingKey}");

                        var properties = channel.CreateBasicProperties();

                        properties.Persistent = true;

                        channel.BasicPublish("topic", routingKey, properties, bodyByte);

                        Console.WriteLine($"{routingKey}  * send!");
                    }
                }
            }
        }
    }
}



