using Withdraw.Cash.Contracts.Messages;

namespace Withdraw.Cash.Application.Abstractions;
public interface IRabbitMQPublisher<T>
    where T : IMessage
{
    Task PublishMessageAsync(T message, string queueName);
}
