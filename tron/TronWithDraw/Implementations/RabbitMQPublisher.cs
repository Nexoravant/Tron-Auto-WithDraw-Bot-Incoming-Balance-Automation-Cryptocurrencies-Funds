using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Withdraw.Cash.Application.Abstractions;
using Withdraw.Cash.Contracts.Messages;
using Withdraw.Cash.Contracts.Settings;

namespace Withdraw.Cash.Application.Implementations;
public class RabbitMQPublisher<TMessage> : IRabbitMQPublisher<TMessage>
    where TMessage : IMessage
{
    private readonly RabbitMQSettings _rabbitMqSetting;

    public RabbitMQPublisher(IOptions<RabbitMQSettings> rabbitMqSetting)
    {
        _rabbitMqSetting = rabbitMqSetting.Value;
    }

    public async Task PublishMessageAsync(TMessage message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqSetting.HostName ?? "localhost",
            UserName = _rabbitMqSetting.UserName ?? "guest",
            Password = _rabbitMqSetting.Password ?? "guest",
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var messageJson = JsonSerializer.Serialize(message);
        var body = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(messageJson));

        BasicProperties props = new ()
        {
            ContentType = "text/plain",
            DeliveryMode = DeliveryModes.Persistent
        };

        await Task.Run(async () => 
        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: false,
            basicProperties: props,
            body: body));
    }
}
