using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Sender
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "127.0.0.1" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest", false, false, false, null);

                    string message = ".NET Core RabbitMQ simple message.";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "BasicTest", null, body);
                    Console.WriteLine($"Sent message {message}.");

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
