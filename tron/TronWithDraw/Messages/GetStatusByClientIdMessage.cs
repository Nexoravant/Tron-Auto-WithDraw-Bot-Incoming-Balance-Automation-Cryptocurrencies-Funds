namespace Withdraw.Cash.Contracts.Messages;

public record GetStatusByClientIdMessage(Guid ClientId, string DepartmentAddress) : IMessage;