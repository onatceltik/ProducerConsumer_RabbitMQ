using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            
            using (var connection = factory.CreateConnection()) // Opens connection
            using (var channel = connection.CreateModel()) // Opens channel
            {
                channel.QueueDeclare("BasicTestQueue", false, false, false, null); // Queue declaring

                // this is the message that we send to our created queue
                string message = "Getting started with .Net Core RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                // Publish message       v*** Queue ****v 
                channel.BasicPublish("", "BasicTestQueue", null, body);
                Console.WriteLine("Sent message {0}...", message);
            }

            Console.WriteLine("Press [enter] to exit the Sender App ...");
            Console.ReadLine();
        }
    }
}
