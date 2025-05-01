using MediatR;
using Withdraw.Cash.Application.Abstractions;
using Withdraw.Cash.Contracts.CashWithdraw.CreateCashWithdrawRequest;
using Withdraw.Cash.Contracts.Constants;
using Withdraw.Cash.Contracts.Messages;

namespace Withdraw.Cash.Application.Commands.CreateCashWithdrawRequest;

public class CreateCashWithdrawRequestCommandHandler : IRequestHandler<CreateCashWithdrawRequestCommand, CreateCashWithdrawRequestResponse>
{
    private readonly IRabbitMQPublisher<CreateCashWithdrawRequestMessage> _publisher;
    
    public CreateCashWithdrawRequestCommandHandler(IRabbitMQPublisher<CreateCashWithdrawRequestMessage> publisher)
    {
        _publisher = publisher;
    }

    public async Task<CreateCashWithdrawRequestResponse> Handle(CreateCashWithdrawRequestCommand request, CancellationToken cancellationToken = default)
    {
        var withdrawRequestId = Guid.NewGuid();

        await _publisher.PublishMessageAsync(
            new CreateCashWithdrawRequestMessage(
                request.ClientId, 
                request.DepartmentAddress, 
                request.Amount,
                request.Currency, 
                request.ClientIp), 
            RabbitMQQueues.CreateCashWithdrawRequestQueue);        
        
        return new CreateCashWithdrawRequestResponse(withdrawRequestId);
    }
}