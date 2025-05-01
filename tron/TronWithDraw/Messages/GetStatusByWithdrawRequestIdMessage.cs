namespace Withdraw.Cash.Contracts.Messages;

public record GetStatusByWithdrawRequestIdMessage(Guid WithdrawRequestId) : IMessage;