using MediatR;
using Withdraw.Cash.Application.Abstractions;
using Withdraw.Cash.Contracts.CashWithdraw.GetStatusByClientId;
using Withdraw.Cash.Contracts.Constants;
using Withdraw.Cash.Contracts.Messages;

namespace Withdraw.Cash.Application.Queries.GetStatusByClientId;

public class GetStatusByClientIdQueryHandler : IRequestHandler<GetStatusByClientIdQuery, GetStatusByClientIdResponse>
{
    private readonly IRabbitMQPublisher<GetStatusByClientIdMessage> _publisher;
    
    public GetStatusByClientIdQueryHandler(IRabbitMQPublisher<GetStatusByClientIdMessage> publisher)
    {
        _publisher = publisher;
    }

    public async Task<GetStatusByClientIdResponse> Handle(GetStatusByClientIdQuery request, CancellationToken cancellationToken)
    {
        await _publisher.PublishMessageAsync(new GetStatusByClientIdMessage(request.ClientId, request.DepartmentAddress), RabbitMQQueues.GetStatusByClientIdQueue);

        return null;
    }
}