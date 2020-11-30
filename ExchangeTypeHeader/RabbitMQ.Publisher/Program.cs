using RabbitMQ.Client;
using System;
using System.Collections.Generic;
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
                    channel.ExchangeDeclare("header", durable:true,type:ExchangeType.Headers);
                    var properties = channel.CreateBasicProperties();

                    Dictionary<string, object> headers = new Dictionary<string, object>();
                    headers.Add("format", "pdf");
                    headers.Add("shape", "a4");

                    channel.BasicPublish("header", "", properties, Encoding.UTF8.GetBytes("My message"));

                    Console.WriteLine("message send!");

                    Console.ReadLine();
                }
            }
        }
    }
}



