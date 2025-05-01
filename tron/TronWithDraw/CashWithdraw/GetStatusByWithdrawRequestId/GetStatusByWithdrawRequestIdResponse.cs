using Withdraw.Cash.Contracts.DTOs;
using Withdraw.Cash.Domain.Entities;

namespace Withdraw.Cash.Contracts.CashWithdraw.GetStatusByWithdrawRequestId;
public record GetStatusByWithdrawRequestIdResponse(Guid RequestId, WithdrawStatus Status, CurrencyDto Currency, decimal Amount);
