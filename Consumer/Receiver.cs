using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection()) // Opens connection
            using (var channel = connection.CreateModel()) // Opens channel
            {
                channel.QueueDeclare("BasicTestQueue", false, false, false, null); // Queue declaring

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (model, ea) =>
                 {
                     var body = ea.Body.Span;
                     var message = Encoding.UTF8.GetString(body);
                     Console.WriteLine("Received message: {0}", message);
                 };

                // Get the message from queue and send it to consumer
                channel.BasicConsume("BasicTestQueue", true, consumer);
            }

            Console.WriteLine("Press [enter] to exit the Consumer App ...");
            Console.ReadLine();
        }
    }
}
