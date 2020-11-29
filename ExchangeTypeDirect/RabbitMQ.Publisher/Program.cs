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
                    channel.ExchangeDeclare("direct", durable:true,type:ExchangeType.Direct);

                    Array logNameArray = Enum.GetValues(typeof(LogNames));

                    for (int i = 1; i <= count; i++)
                    {
                        Random rnd = new Random();

                        LogNames log = (LogNames)logNameArray.GetValue(rnd.Next(logNameArray.Length));

                        var bodyByte = Encoding.UTF8.GetBytes($"{log.ToString()}");

                        var properties = channel.CreateBasicProperties();

                        properties.Persistent = true;

                        channel.BasicPublish("direct", log.ToString(), properties, bodyByte);

                        Console.WriteLine($"{log.ToString()}  * send!");
                    }
                }
            }
        }
    }
}



