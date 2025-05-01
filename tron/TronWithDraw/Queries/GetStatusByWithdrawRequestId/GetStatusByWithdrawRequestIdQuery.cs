using MediatR;
using Withdraw.Cash.Contracts.CashWithdraw.GetStatusByWithdrawRequestId;

namespace Withdraw.Cash.Application.Queries.GetStatusByWithdrawRequestId;

public record GetStatusByWithdrawRequestIdQuery(Guid WithdrawRequestId) : IRequest<GetStatusByWithdrawRequestIdResponse>;
