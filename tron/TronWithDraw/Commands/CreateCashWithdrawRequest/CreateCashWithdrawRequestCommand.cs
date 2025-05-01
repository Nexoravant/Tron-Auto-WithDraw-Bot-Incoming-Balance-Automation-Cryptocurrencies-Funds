using MediatR;
using Withdraw.Cash.Contracts.CashWithdraw.CreateCashWithdrawRequest;
using Withdraw.Cash.Contracts.DTOs;

namespace Withdraw.Cash.Application.Commands.CreateCashWithdrawRequest;

public record CreateCashWithdrawRequestCommand(
    Guid ClientId,
    string DepartmentAddress,
    decimal Amount,
    CurrencyDto Currency,
    string ClientIp) : IRequest<CreateCashWithdrawRequestResponse>, IRequest<Guid>;