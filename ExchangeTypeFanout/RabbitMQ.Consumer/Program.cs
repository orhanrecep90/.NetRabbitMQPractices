﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            int time = int.Parse(args[0].ToString());
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("your amqp url");
            //for local RabbitMq installation
            //factory.HostName = "localhost";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare("test", durable: true, type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queueName,exchange:"test",routingKey:"");

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


