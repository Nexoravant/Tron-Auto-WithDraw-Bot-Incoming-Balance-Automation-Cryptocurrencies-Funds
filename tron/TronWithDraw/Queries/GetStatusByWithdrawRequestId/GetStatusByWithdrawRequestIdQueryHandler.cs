using MediatR;
using Withdraw.Cash.Application.Abstractions;
using Withdraw.Cash.Contracts.CashWithdraw.GetStatusByWithdrawRequestId;
using Withdraw.Cash.Contracts.Constants;
using Withdraw.Cash.Contracts.Messages;

namespace Withdraw.Cash.Application.Queries.GetStatusByWithdrawRequestId;

public class GetStatusByWithdrawRequestIdQueryHandler : IRequestHandler<GetStatusByWithdrawRequestIdQuery, GetStatusByWithdrawRequestIdResponse>
{
    private readonly IRabbitMQPublisher<GetStatusByWithdrawRequestIdMessage> _publisher;

    public GetStatusByWithdrawRequestIdQueryHandler(IRabbitMQPublisher<GetStatusByWithdrawRequestIdMessage> publisher)
    {
        _publisher = publisher;
    }

    public async Task<GetStatusByWithdrawRequestIdResponse> Handle(GetStatusByWithdrawRequestIdQuery request, CancellationToken cancellationToken)
    {
        await _publisher.PublishMessageAsync(new GetStatusByWithdrawRequestIdMessage(request.WithdrawRequestId),
            RabbitMQQueues.GetStatusByWithdrawRequestIdQueue);

        return null;
    }
}