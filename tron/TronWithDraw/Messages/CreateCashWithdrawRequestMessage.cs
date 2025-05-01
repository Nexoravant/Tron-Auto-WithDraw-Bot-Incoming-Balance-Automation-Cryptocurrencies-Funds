using Withdraw.Cash.Contracts.DTOs;

namespace Withdraw.Cash.Contracts.Messages;

public record CreateCashWithdrawRequestMessage(
    Guid ClientId,
    string DepartmentAddress,
    decimal Amount,
    CurrencyDto Currency,
    string ClientIp) : IMessage;
