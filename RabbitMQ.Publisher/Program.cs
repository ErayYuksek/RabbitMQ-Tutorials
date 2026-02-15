using RabbitMQ.Client;
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

var message = "Merhaba Eray";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(
    exchange: "",
    routingKey: "example-queue",
    basicProperties: null,
    body: body);

Console.WriteLine("Mesaj gönderildi!");

Console.ReadLine();