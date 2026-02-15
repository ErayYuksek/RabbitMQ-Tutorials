using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672/");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "example-queue",
    durable: true,
    exclusive: false,
    autoDelete: false);

Console.WriteLine("Mesaj bekleniyor...");

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(
    queue: "example-queue",
    autoAck: false,
    consumer: consumer);

consumer.Received += (model, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine("Gelen mesaj: " + message);

    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();