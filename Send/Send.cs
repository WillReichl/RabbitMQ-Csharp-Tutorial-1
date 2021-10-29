using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Send
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            
            // The connection abstracts the socket connection, and takes care of protocol version negotiation and 
            // authentication and so on for us. Here we connect to a RabbitMQ node on the local machine - hence 
            // the localhost. If we wanted to connect to a node on a different machine we'd simply specify 
            // its hostname or IP address here.            
            using var connection = factory.CreateConnection();
            // Next we create a channel, which is where most of the API for getting things done resides.
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);

            Console.WriteLine(" [x] Sent {0}", message);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
