using Withdraw.Cash.Contracts.DTOs;

namespace Withdraw.Cash.Contracts.CashWithdraw.CreateCashWithdrawRequest;
public record CreateCashWithdrawRequest(
    Guid ClientId,
    string DepartmentAddress,
    decimal Amount,
    CurrencyDto Currency);
