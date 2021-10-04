using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "127.0.0.1" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest", false, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                        Console.WriteLine($"Received message {message}.");
                    };

                    channel.BasicConsume("BasicTest", true, consumer);

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
